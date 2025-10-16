using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public class CartValidationService : ICartValidationService
{
    public Task<Result> ValidateGroupAttributes(Product product, List<SelectedGroupAttribute> selectedGroups)
    {
        var errors = new List<string>();

        foreach (var selectedGroup in selectedGroups)
        {
            var productGroup = product.GroupAttributes.FirstOrDefault(g => g.GroupAttributeId == selectedGroup.GroupAttributeId);
            if (productGroup == null)
            {
                errors.Add($"El grupo {selectedGroup.GroupAttributeId} no existe en el producto");
                continue;
            }

            var totalSelectedAttributes = selectedGroup.Attributes.Sum(a => a.Quantity);
            var verifyValue = productGroup.QuantityInformation.VerifyValue;
            var requiredQuantity = productGroup.QuantityInformation.GroupAttributeQuantity;

            if (verifyValue == "EQUAL_THAN" && totalSelectedAttributes != requiredQuantity)
            {
                errors.Add($"El grupo '{productGroup.GroupAttributeType.Name}' requiere exactamente {requiredQuantity} selección(es), pero se enviaron {totalSelectedAttributes}");
            }
            else if (verifyValue == "LOWER_EQUAL_THAN" && totalSelectedAttributes > requiredQuantity)
            {
                errors.Add($"El grupo '{productGroup.GroupAttributeType.Name}' permite máximo {requiredQuantity} selección(es), pero se enviaron {totalSelectedAttributes}");
            }

            foreach (var selectedAttr in selectedGroup.Attributes)
            {
                var productAttr = productGroup.Attributes.FirstOrDefault(a => a.AttributeId == selectedAttr.AttributeId);
                if (productAttr == null)
                {
                    errors.Add($"El atributo {selectedAttr.AttributeId} no existe en el grupo {selectedGroup.GroupAttributeId}");
                    continue;
                }

                if (selectedAttr.Quantity > productAttr.MaxQuantity)
                {
                    errors.Add($"El atributo '{productAttr.Name}' permite máximo {productAttr.MaxQuantity} unidad(es), pero se enviaron {selectedAttr.Quantity}");
                }

                if (selectedAttr.Quantity < 0)
                {
                    errors.Add($"El atributo '{productAttr.Name}' no puede tener cantidad negativa");
                }
            }
        }

        var mandatoryGroups = product.GroupAttributes.Where(g => g.QuantityInformation.VerifyValue == "EQUAL_THAN");
        errors.AddRange(
            from mandatoryGroup in mandatoryGroups 
            let selectedGroup = selectedGroups
                .FirstOrDefault(g => g.GroupAttributeId == mandatoryGroup.GroupAttributeId) 
            where selectedGroup == null || selectedGroup.Attributes.Count == 0
            select $"El grupo obligatorio '{mandatoryGroup.GroupAttributeType.Name}' debe ser seleccionado");

        return Task.FromResult(errors.Count > 0 ? Result.Failure(errors) : Result.Success());
    }
}