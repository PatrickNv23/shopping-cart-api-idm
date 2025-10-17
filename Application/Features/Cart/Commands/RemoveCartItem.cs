using MediatR;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Application.Features.Cart.Commands;

public record RemoveCartItemCommand(string CartItemId) : IRequest<Result>;

public class RemoveCartItemCommandHandler(ICartRepository cartRepository)
    : IRequestHandler<RemoveCartItemCommand, Result>
{
    public async Task<Result> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var item = await cartRepository.GetItemAsync(request.CartItemId);
        if (item == null)
            return Result.Failure("Item no encontrado en el carrito");

        await cartRepository.RemoveItemAsync(request.CartItemId);
        return Result.Success();
    }
}