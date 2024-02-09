using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class ProductColor : BaseEntity<Guid>, IAuditable
{
	public ProductModel Product { get; set; }
	public Guid ProductId { get; set; }


	public Color Color { get; set; }
	public Guid ColorId { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
