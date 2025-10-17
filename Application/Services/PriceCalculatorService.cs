using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public class PriceCalculationService : IPriceCalculationService
{
    public decimal CalculateTotalPrice(Product product, List<SelectedGroupAttribute> selectedGroups, int quantity)
    {
        decimal itemPrice = product.Price;

        foreach (var selectedGroup in selectedGroups)
        {
            var productGroup = product.GroupAttributes.FirstOrDefault(g => g.GroupAttributeId == selectedGroup.GroupAttributeId);
            if (productGroup == null) continue;

            foreach (var selectedAttr in selectedGroup.Attributes)
            {
                var productAttr = productGroup.Attributes.FirstOrDefault(a => a.AttributeId == selectedAttr.AttributeId);
                if (productAttr != null)
                {
                    itemPrice += productAttr.PriceImpactAmount * selectedAttr.Quantity;
                }
            }
        }

        return itemPrice * quantity;
    }
}