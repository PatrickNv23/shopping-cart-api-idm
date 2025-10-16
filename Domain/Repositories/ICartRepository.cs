using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart> GetCartAsync();
    Task<CartItem?> GetItemAsync(string cartItemId);
    Task AddItemAsync(CartItem item);
    Task UpdateItemAsync(CartItem item);
    Task RemoveItemAsync(string cartItemId);
}