using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public interface IPriceCalculationService
{
    decimal CalculateTotalPrice(Product product, List<SelectedGroupAttribute> selectedGroups, int quantity);
}