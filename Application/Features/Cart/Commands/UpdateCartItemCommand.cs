using MediatR;
using ShoppingCartIDM.Application.Services;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Application.Features.Cart.Commands;

public record UpdateCartItemCommand(
    string CartItemId,
    int Quantity,
    List<SelectedGroupAttribute> GroupAttributes
) : IRequest<Result<CartItem>>;

public class UpdateCartItemCommandHandler(
    IProductRepository productRepository,
    ICartRepository cartRepository,
    ICartValidationService validationService,
    IPriceCalculationService priceCalculationService)
    : IRequestHandler<UpdateCartItemCommand, Result<CartItem>>
{
    public async Task<Result<CartItem>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var existingItem = await cartRepository.GetItemAsync(request.CartItemId);
        if (existingItem == null)
            return Result<CartItem>.Failure("Item no encontrado en el carrito");

        var product = await productRepository.GetByIdAsync(existingItem.ProductId);
        if (product == null)
            return Result<CartItem>.Failure("Producto no encontrado");

        var validationResult = await validationService.ValidateGroupAttributes(product, request.GroupAttributes);
        if (!validationResult.IsSuccess)
            return Result<CartItem>.Failure(validationResult.Errors);

        var totalPrice = priceCalculationService.CalculateTotalPrice(product, request.GroupAttributes, request.Quantity);

        existingItem.Quantity = request.Quantity;
        existingItem.TotalPrice = totalPrice;
        existingItem.GroupAttributes = request.GroupAttributes;
        existingItem.UpdatedAt = DateTime.UtcNow;

        await cartRepository.UpdateItemAsync(existingItem);
        return Result<CartItem>.Success(existingItem);
    }
}