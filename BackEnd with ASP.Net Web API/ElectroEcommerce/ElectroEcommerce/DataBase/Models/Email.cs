using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class Email : BaseEntity<Guid>, IAuditable
{
	public List<string> Recipients { get; set; } = new List<string>();
	public string Subject { get; set; } = string.Empty;
	public string Body { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid UserId { get; set; }
	public User User { get; set; }
}
