using MediatR;
using ShoppingCartIDM.Application.Services;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Application.Features.Cart.Commands;

public record AddCartItemCommand(
    int ProductId,
    int Quantity,
    List<SelectedGroupAttribute> GroupAttributes
) : IRequest<Result<CartItem>>;

public class AddCartItemCommandHandler(
    IProductRepository productRepository,
    ICartRepository cartRepository,
    ICartValidationService validationService,
    IPriceCalculationService priceCalculationService)
    : IRequestHandler<AddCartItemCommand, Result<CartItem>>
{
    public async Task<Result<CartItem>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            return Result<CartItem>.Failure("Producto no encontrado");

        var validationResult = await validationService.ValidateGroupAttributes(product, request.GroupAttributes);
        if (!validationResult.IsSuccess)
            return Result<CartItem>.Failure(validationResult.Errors);

        var totalPrice = priceCalculationService.CalculateTotalPrice(product, request.GroupAttributes, request.Quantity);

        var cartItem = new CartItem
        {
            ProductId = request.ProductId,
            ProductName = product.Name,
            Quantity = request.Quantity,
            BasePrice = product.Price,
            TotalPrice = totalPrice,
            GroupAttributes = request.GroupAttributes
        };

        await cartRepository.AddItemAsync(cartItem);
        return Result<CartItem>.Success(cartItem);
    }
}