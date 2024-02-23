using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class OrderItem : BaseEntity<Guid>, IAuditable
{
	internal List<string> ProductOrderPhotos;

	public Guid OrderId { get; set; }
	public Order Order { get; set; }
	public string ProductName { get; set; }
	public decimal ProductPrice { get; set; }
	public string ProductPrefix { get; set; } = string.Empty;
	public string ProductDescription { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid ProductID { get; set; }
	public Guid ProductColorID { get; set; }
	public int Quantity { get; set; }
	public decimal OrderItemTotalPrice { get; set; }
	public decimal OrderItemSinglePrice { get; set; }
	public string PhisicalImageName { get; set; } = string.Empty;
}
