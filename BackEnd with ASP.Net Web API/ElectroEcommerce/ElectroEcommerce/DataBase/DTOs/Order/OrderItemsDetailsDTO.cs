namespace ElectroEcommerce.DataBase.DTOs.Order;

public class OrderItemsDetailsDTO
{
	public string ProductCode { get; set; } = string.Empty;
	public string ProductName { get; set; } = string.Empty;
	public string BrandName { get; set; } = string.Empty;
	public string CategoryName { get; set; } = string.Empty;
	public decimal OrderItemTotalPrice { get; set; }
	public decimal OrderItemSinglePrice { get; set; }
	public int Quantity { get; set; }
	public string PhisicalImageURL { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
}
