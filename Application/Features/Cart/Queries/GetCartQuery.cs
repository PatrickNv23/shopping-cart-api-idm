using MediatR;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Application.Features.Cart.Queries;

public record GetCartQuery : IRequest<Result<Domain.Entities.Cart>>;

public class GetCartQueryHandler(ICartRepository cartRepository)
    : IRequestHandler<GetCartQuery, Result<Domain.Entities.Cart>>
{
    public async Task<Result<Domain.Entities.Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync();
        return Result<Domain.Entities.Cart>.Success(cart);
    }
}   