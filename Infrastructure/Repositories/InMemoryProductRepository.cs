using ShoppingCartIDM.Domain.Entities;
using ShoppingCartIDM.Domain.Repositories;

namespace ShoppingCartIDM.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<int, Product> _products;

    public InMemoryProductRepository()
    {
        _products = new Dictionary<int, Product>
        {
            [3546345] = CreateSampleProduct()
        };
    }

    public Task<Product?> GetByIdAsync(int productId)
    {
        _products.TryGetValue(productId, out var product);
        return Task.FromResult(product);
    }

    private static Product CreateSampleProduct()
    {
        return new Product
        {
            ProductId = 3546345,
            Name = "Mi Producto",
            Price = 11.90m,
            GroupAttributes = new List<GroupAttribute>
            {
                new()
                {
                    GroupAttributeId = "241887",
                    GroupAttributeType = new() { GroupAttributeTypeId = "1826", Name = "Elige el tipo de pan" },
                    Description = "Tipo de pan",
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 1,
                        ShowPricePerProduct = true,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 13300, AttributeId = 968636, Name = "Pan Premium", DefaultQuantity = 1, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13301, AttributeId = 968637, Name = "Pan Grande", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" }
                    },
                    Order = 1
                },
                new()
                {
                    GroupAttributeId = "241888",
                    GroupAttributeType = new() { GroupAttributeTypeId = "1848", Name = "¿Deseas más carnes? (opcional)" },
                    Description = "Ad Carnes",
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 4,
                        ShowPricePerProduct = true,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "LOWER_EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 1322, AttributeId = 968639, Name = "Carne XT", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 6, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13176, AttributeId = 968640, Name = "Carne Vegetal", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 5, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 1325, AttributeId = 968641, Name = "Carne Brava", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 4, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 1324, AttributeId = 968642, Name = "Carne Tradicional", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 4, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13174, AttributeId = 968643, Name = "Pollo", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 3, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 4230, AttributeId = 968644, Name = "Chorizo", DefaultQuantity = 0, MaxQuantity = 4, PriceImpactAmount = 3, IsRequired = false, StatusId = "A" }
                    },
                    Order = 2
                },
                new()
                {
                    GroupAttributeId = "241889",
                    GroupAttributeType = new() { GroupAttributeTypeId = "90", Name = "Personaliza tus toppings (opcional)" },
                    Description = "Ad Toppings",
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 5,
                        ShowPricePerProduct = true,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "LOWER_EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 6281, AttributeId = 968646, Name = "Queso americano (2 lascas)", DefaultQuantity = 1, MaxQuantity = 5, PriceImpactAmount = 2, IsRequired = true, NegativeAttributeId = "963", StatusId = "A" },
                        new() { ProductId = 950, AttributeId = 968647, Name = "Tocino (2 lascas)", DefaultQuantity = 1, MaxQuantity = 5, PriceImpactAmount = 2, IsRequired = true, NegativeAttributeId = "965", StatusId = "A" },
                        new() { ProductId = 954, AttributeId = 968648, Name = "Tomate", DefaultQuantity = 1, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = true, NegativeAttributeId = "968", StatusId = "A" },
                        new() { ProductId = 953, AttributeId = 968649, Name = "Lechuga", DefaultQuantity = 1, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = true, NegativeAttributeId = "967", StatusId = "A" },
                        new() { ProductId = 6629, AttributeId = 968650, Name = "Papas al hilo", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 1, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 956, AttributeId = 968652, Name = "Pickles", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 955, AttributeId = 968653, Name = "Cebolla", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" }
                    },
                    Order = 3
                },
                new()
                {
                    GroupAttributeId = "241890",
                    GroupAttributeType = new() { GroupAttributeTypeId = "1854", Name = "Personaliza tus salsas (opcional)" },
                    Description = "Ad Salsas",
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 5,
                        ShowPricePerProduct = true,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "LOWER_EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 957, AttributeId = 968655, Name = "Mayonesa", DefaultQuantity = 1, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = true, NegativeAttributeId = "971", StatusId = "A" },
                        new() { ProductId = 961, AttributeId = 968656, Name = "Salsa Stacker", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 1, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 960, AttributeId = 968657, Name = "Salsa BBQ", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 1, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 959, AttributeId = 968658, Name = "Ketchup", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 958, AttributeId = 968659, Name = "Mostaza", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 6630, AttributeId = 968660, Name = "Ají", DefaultQuantity = 0, MaxQuantity = 5, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13179, AttributeId = 968661, Name = "Salsa Stacker Gratis", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13178, AttributeId = 968662, Name = "Salsa BBQ Gratis", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" }
                    },
                    Order = 4
                },
                new()
                {
                    GroupAttributeId = "241891",
                    GroupAttributeType = new() { GroupAttributeTypeId = "106", Name = "Elige tu complemento" },
                    Description = null,
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 1,
                        ShowPricePerProduct = true,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 930, AttributeId = 968663, Name = "Papa Personal", DefaultQuantity = 1, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 10459, AttributeId = 968664, Name = "Papa Familiar", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 3, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 11748, AttributeId = 968665, Name = "Papa Tumbay Mediana", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 1, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 11877, AttributeId = 968666, Name = "Papa Tumbay Familiar", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 4, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13295, AttributeId = 968667, Name = "Camote Mediano", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 1, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 13298, AttributeId = 968668, Name = "Camote Familiar", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 4, IsRequired = false, StatusId = "A" }
                    },
                    Order = 5
                },
                new()
                {
                    GroupAttributeId = "241892",
                    GroupAttributeType = new() { GroupAttributeTypeId = "91", Name = "Elige tu bebida" },
                    Description = null,
                    QuantityInformation = new()
                    {
                        GroupAttributeQuantity = 1,
                        ShowPricePerProduct = false,
                        IsShown = true,
                        IsEditable = true,
                        IsVerified = true,
                        VerifyValue = "EQUAL_THAN"
                    },
                    Attributes = new List<ProductAttribute>
                    {
                        new() { ProductId = 935, AttributeId = 968670, Name = "Coca-Cola Sin Azúcar 500 ml", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 938, AttributeId = 968671, Name = "Inca Kola Sin Azúcar 500 ml", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 939, AttributeId = 968674, Name = "Fanta 500 ml", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" },
                        new() { ProductId = 933, AttributeId = 968677, Name = "Agua San Luis Sin Gas 625 ml", DefaultQuantity = 0, MaxQuantity = 1, PriceImpactAmount = 0, IsRequired = false, StatusId = "A" }
                    },
                    Order = 6
                }
            }
        };
    }
}