namespace ShoppingCartIDM.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<GroupAttribute> GroupAttributes { get; set; } = [];
}

public class GroupAttribute
{
    public string GroupAttributeId { get; set; } = string.Empty;
    public GroupAttributeType GroupAttributeType { get; set; } = new();
    public string? Description { get; set; }
    public QuantityInformation QuantityInformation { get; set; } = new();
    public List<ProductAttribute> Attributes { get; set; } = [];
    public int Order { get; set; }
}

public class GroupAttributeType
{
    public string GroupAttributeTypeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class QuantityInformation
{
    public int GroupAttributeQuantity { get; set; }
    public bool ShowPricePerProduct { get; set; }
    public bool IsShown { get; set; }
    public bool IsEditable { get; set; }
    public bool IsVerified { get; set; }
    public string VerifyValue { get; set; } = string.Empty;
}

public class ProductAttribute
{
    public int ProductId { get; set; }
    public int AttributeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public decimal PriceImpactAmount { get; set; }
    public bool IsRequired { get; set; }
    public string? NegativeAttributeId { get; set; }
    public int Order { get; set; }
    public string StatusId { get; set; } = string.Empty;
    public string? UrlImage { get; set; }
}