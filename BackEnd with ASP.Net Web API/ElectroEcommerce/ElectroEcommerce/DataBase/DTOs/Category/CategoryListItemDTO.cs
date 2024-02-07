using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.DTOs.Category;

public class CategoryListItemDTO : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime CreatedAt { get ; set; }
	public DateTime UpdatedAt { get ; set; }
}
