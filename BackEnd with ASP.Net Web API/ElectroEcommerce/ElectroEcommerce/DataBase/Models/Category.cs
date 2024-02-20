using ElectroEcommerce.DataBase.Base;
using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.Models;

public class Category : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<ProductModel> ? Products { get; set; }
}
