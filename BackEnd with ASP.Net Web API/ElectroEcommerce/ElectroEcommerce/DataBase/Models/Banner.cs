using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class Banner : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string BannerPrefix { get; set; }
	public List<string> Files { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set ; }
}
