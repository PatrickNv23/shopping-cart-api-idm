using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Controllers.Dtos;

public record AddCartItemRequest(
    int ProductId,
    int Quantity,
    List<SelectedGroupAttribute> GroupAttributes
);

public record UpdateCartItemRequest(
    int Quantity,
    List<SelectedGroupAttribute> GroupAttributes
);

public record AdjustQuantityRequest(int Adjustment);