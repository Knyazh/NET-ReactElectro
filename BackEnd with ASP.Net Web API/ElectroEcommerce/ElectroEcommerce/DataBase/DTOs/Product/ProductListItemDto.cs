using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.DTOs.Product
{
	public class ProductListItemDto : BaseEntity<Guid>, IAuditable
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string PhisicalImageName { get; set; } = string.Empty;
		public string ProductPrefix {  get; set; } = string.Empty;
		public DateTime CreatedAt { get ; set ; }
		public DateTime UpdatedAt { get ; set; }
	}
}
