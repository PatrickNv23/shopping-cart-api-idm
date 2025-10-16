namespace ShoppingCartIDM.Domain.Entities;

public class Cart
{
    public List<CartItem> Items { get; set; } = [];
    public decimal Subtotal => Items.Sum(i => i.TotalPrice);
    public int TotalItems => Items.Sum(i => i.Quantity);
}