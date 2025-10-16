using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public class PriceCalculationService : IPriceCalculationService
{
    public decimal CalculateTotalPrice(Product product, List<SelectedGroupAttribute> selectedGroups, int quantity)
    {
        var itemPrice = product.Price;
        
        selectedGroups.ForEach(selectedGroup =>
        {
            var productGroup = product.GroupAttributes.FirstOrDefault(g => g.GroupAttributeId == selectedGroup.GroupAttributeId);
            if (productGroup == null) return;
            
            selectedGroup.Attributes.ForEach(selectedAttr =>
            {
                var productAttr = productGroup.Attributes.FirstOrDefault(a => a.AttributeId == selectedAttr.AttributeId);
                if (productAttr != null)
                {
                    itemPrice += productAttr.PriceImpactAmount * selectedAttr.Quantity;
                }
            });
        });
        
        return itemPrice * quantity;
    }
}