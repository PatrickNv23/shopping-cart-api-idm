using ShoppingCartIDM.Domain.Entities;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Infrastructure.Repositories;

public class InMemoryCartRepository : ICartRepository
{
    private readonly Cart _cart = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<Cart> GetCartAsync()
    {
        await _lock.WaitAsync();
        try
        {
            return _cart;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<CartItem?> GetItemAsync(string cartItemId)
    {
        await _lock.WaitAsync();
        try
        {
            return _cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task AddItemAsync(CartItem item)
    {
        await _lock.WaitAsync();
        try
        {
            _cart.Items.Add(item);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task UpdateItemAsync(CartItem item)
    {
        await _lock.WaitAsync();
        try
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.CartItemId == item.CartItemId);
            if (existingItem != null)
            {
                var index = _cart.Items.IndexOf(existingItem);
                _cart.Items[index] = item;
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task RemoveItemAsync(string cartItemId)
    {
        await _lock.WaitAsync();
        try
        {
            var item = _cart.Items.FirstOrDefault(i => i.CartItemId == cartItemId);
            if (item != null)
            {
                _cart.Items.Remove(item);
            }
        }
        finally
        {
            _lock.Release();
        }
    }
}