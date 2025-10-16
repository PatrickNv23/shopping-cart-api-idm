using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);
}