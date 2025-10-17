using MediatR;
using ShoppingCartIDM.Application.Services;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Application.Features.Cart.Commands;

public record AdjustQuantityCommand(
    string CartItemId,
    int Adjustment
) : IRequest<Result<CartItem>>;

public class AdjustQuantityCommandHandler(
    IProductRepository productRepository,
    ICartRepository cartRepository,
    IPriceCalculationService priceCalculationService)
    : IRequestHandler<AdjustQuantityCommand, Result<CartItem>>
{
    public async Task<Result<CartItem>> Handle(AdjustQuantityCommand request, CancellationToken cancellationToken)
    {
        var item = await cartRepository.GetItemAsync(request.CartItemId);
        if (item == null)
            return Result<CartItem>.Failure("Item no encontrado en el carrito");

        var newQuantity = item.Quantity + request.Adjustment;
        if (newQuantity <= 0)
            return Result<CartItem>.Failure("La cantidad debe ser mayor a 0");

        var product = await productRepository.GetByIdAsync(item.ProductId);
        if (product == null)
            return Result<CartItem>.Failure("Producto no encontrado");

        item.Quantity = newQuantity;
        item.TotalPrice = priceCalculationService.CalculateTotalPrice(product, item.GroupAttributes, newQuantity);
        item.UpdatedAt = DateTime.UtcNow;

        await cartRepository.UpdateItemAsync(item);
        return Result<CartItem>.Success(item);
    }
}