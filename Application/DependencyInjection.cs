using ShoppingCartIDM.Application.Services;

namespace ShoppingCartIDM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICartValidationService, CartValidationService>();
        services.AddScoped<IPriceCalculationService, PriceCalculationService>();
        
        return services;
    }
}