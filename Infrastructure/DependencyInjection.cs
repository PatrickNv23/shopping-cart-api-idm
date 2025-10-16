using ShoppingCartIDM.Domain.Repositories;
using ShoppingCartIDM.Infrastructure.Repositories;

namespace ShoppingCartIDM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddSingleton<ICartRepository, InMemoryCartRepository>();
        
        return services;
    }
}