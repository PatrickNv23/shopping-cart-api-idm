using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartIDM.Application.Features.Cart.Commands;
using ShoppingCartIDM.Application.Features.Cart.Queries;
using ShoppingCartIDM.Common;
using ShoppingCartIDM.Controllers.Dtos;
using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(IMediator mediator) : ControllerBase
{
    [HttpPost("items")]
    public async Task<ActionResult<Result<CartItem>>> AddItem([FromBody] AddCartItemRequest request)
    {
        var command = new AddCartItemCommand(
            request.ProductId,
            request.Quantity,
            request.GroupAttributes
        );

        var result = await mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPut("items/{cartItemId}")]
    public async Task<ActionResult<Result<CartItem>>> UpdateItem(
        string cartItemId,
        [FromBody] UpdateCartItemRequest request)
    {
        var command = new UpdateCartItemCommand(
            cartItemId,
            request.Quantity,
            request.GroupAttributes
        );

        var result = await mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPatch("items/{cartItemId}/quantity")]
    public async Task<ActionResult<Result<CartItem>>> AdjustQuantity(
        string cartItemId,
        [FromBody] AdjustQuantityRequest request)
    {
        var command = new AdjustQuantityCommand(cartItemId, request.Adjustment);

        var result = await mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpDelete("items/{cartItemId}")]
    public async Task<ActionResult<Result>> RemoveItem(string cartItemId)
    {
        var command = new RemoveCartItemCommand(cartItemId);

        var result = await mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<Result<Cart>>> GetCart()
    {
        var query = new GetCartQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}