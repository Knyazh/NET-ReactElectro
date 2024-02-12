using ElectroEcommerce.DataBase.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroEcommerce.DataBase.Models;

public class ActivationToken : BaseEntity<Guid> , IAuditable
{
	public string UniqueActivationToken { get; set; } = string.Empty;
	public bool IsUsed { get; set; } = false;
	public DateTime LastUpdatedAt { get; set; }
	[ForeignKey("User")]
	public Guid UserId { get; set; }
	public User User { get; set; }
	public DateTime ExpireDate { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get ; set ; }
}
