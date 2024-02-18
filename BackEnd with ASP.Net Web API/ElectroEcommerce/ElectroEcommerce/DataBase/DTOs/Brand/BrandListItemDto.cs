using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.DTOs.Brand;

public class BrandListItemDto : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string LogoUrl { get; set; } = string.Empty;
	public string BrandPrefix { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
