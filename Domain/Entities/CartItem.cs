namespace ShoppingCartIDM.Domain.Entities;

public class CartItem
{
    public string CartItemId { get; set; } = Guid.NewGuid().ToString();
    public int ProductId { get; init; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public List<SelectedGroupAttribute> GroupAttributes { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class SelectedGroupAttribute
{
    public string GroupAttributeId { get; set; } = string.Empty;
    public List<SelectedAttribute> Attributes { get; set; } = [];
}

public class SelectedAttribute
{
    public int AttributeId { get; set; }
    public int Quantity { get; set; }
}