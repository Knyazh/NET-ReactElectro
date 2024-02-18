using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models
{
	public class Brand : BaseEntity<Guid>, IAuditable
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string BrandPrefix { get; set; }
		public string LogoURL { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public List<ProductModel> Products { get; set; }
	}
}
