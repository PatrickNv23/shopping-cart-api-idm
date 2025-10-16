using ShoppingCartIDM.Common;
using ShoppingCartIDM.Domain.Entities;

namespace ShoppingCartIDM.Application.Services;

public interface ICartValidationService
{
    Task<Result> ValidateGroupAttributes(Product product, List<SelectedGroupAttribute> selectedGroups);
}