using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class Color : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; }
	public DateTime CreatedAt { get ; set ; }
	public DateTime UpdatedAt { get; set ; }
}
