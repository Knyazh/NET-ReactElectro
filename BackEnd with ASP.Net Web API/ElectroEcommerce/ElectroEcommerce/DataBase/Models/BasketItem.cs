using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class BasketItem : BaseEntity<Guid>, IAuditable
{
	public Guid ProductID { get; set; }
	public Guid ColorID { get; set; }
	public int Quantity { get; set; }
	public bool IsAviable { get; set; }
	public string ProductName { get; set; } = string.Empty;
	public string ProductPrefix { get; set; } = string.Empty;
	public string[] PhisicalImageNames { get; set; } = new string[] { };
	public decimal Price { get; set; }
	public User User { get; set; }
	public Guid CurrentUserID { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
