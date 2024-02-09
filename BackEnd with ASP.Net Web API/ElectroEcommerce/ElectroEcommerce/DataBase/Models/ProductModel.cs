using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class ProductModel : BaseEntity<Guid>, IAuditable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;

    public decimal Brand { get; set; } = 0;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

    public List<ProductColor> ProductColors { get; set; }
}
