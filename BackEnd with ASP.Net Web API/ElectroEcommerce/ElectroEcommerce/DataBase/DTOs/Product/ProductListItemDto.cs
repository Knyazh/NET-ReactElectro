using ElectroEcommerce.DataBase.Base;
using ElectroEcommerce.DataBase.Models;

namespace ElectroEcommerce.DataBase.DTOs.Product
{
	public class ProductListItemDto : BaseEntity<Guid>, IAuditable
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Quantity { get; set; }
		public bool IsAvailable { get; set; }
		public decimal Price { get; set; }
		public List<string> PhisicalImageNames { get; set; } = new List<string>();
		public string ProductPrefix {  get; set; } = string.Empty;
		public Brand CurrentBrand { get; set; }
		public List<Color> Colors { get; set; }
		public DateTime CreatedAt { get ; set ; }
		public DateTime UpdatedAt { get ; set; }
	}
}
