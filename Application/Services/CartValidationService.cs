using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public class CartValidationService : ICartValidationService
{
    public Task<Result> ValidateGroupAttributes(Product product, List<SelectedGroupAttribute> selectedGroups)
    {
        var errors = new List<string>();

        // Validar grupos obligatorios primero
        var mandatoryGroups = product.GroupAttributes.Where(g => g.QuantityInformation.VerifyValue == "EQUAL_THAN");
        foreach (var mandatoryGroup in mandatoryGroups)
        {
            var selectedGroup = selectedGroups.FirstOrDefault(g => g.GroupAttributeId == mandatoryGroup.GroupAttributeId);
            if (selectedGroup == null || !selectedGroup.Attributes.Any())
            {
                errors.Add($"El grupo obligatorio '{mandatoryGroup.GroupAttributeType.Name}' debe ser seleccionado");
                continue;
            }
        }

        // Validar cada grupo enviado
        foreach (var selectedGroup in selectedGroups)
        {
            // consistencia de grupos
            var productGroup = product.GroupAttributes.FirstOrDefault(g => g.GroupAttributeId == selectedGroup.GroupAttributeId);
            if (productGroup == null)
            {
                errors.Add($"El grupo {selectedGroup.GroupAttributeId} no existe en el producto");
                continue;
            }
            
            // Validar cantidades de grupos
            var numberOfSelectedAttributes = selectedGroup.Attributes.Count(a => a.Quantity > 0);
            var verifyValue = productGroup.QuantityInformation.VerifyValue;
            var requiredQuantity = productGroup.QuantityInformation.GroupAttributeQuantity;

            if (verifyValue == "EQUAL_THAN" && numberOfSelectedAttributes != requiredQuantity)
            {
                errors.Add($"El grupo '{productGroup.GroupAttributeType.Name}' requiere exactamente {requiredQuantity} selección(es), pero se enviaron {numberOfSelectedAttributes}");
            }
            else if (verifyValue == "LOWER_EQUAL_THAN" && numberOfSelectedAttributes > requiredQuantity)
            {
                errors.Add($"El grupo '{productGroup.GroupAttributeType.Name}' permite máximo {requiredQuantity} selección(es), pero se enviaron {numberOfSelectedAttributes}");
            }

            // Validar cada atributo individualmente
            foreach (var selectedAttr in selectedGroup.Attributes)
            {
                // consistencia de atributos
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

        return Task.FromResult(errors.Any() ? Result.Failure(errors) : Result.Success());
    }
}