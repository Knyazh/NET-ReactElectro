namespace ElectroEcommerce.DataBase.DTOs.Order;

public class OrderDetailsDTO
{
	public Guid OrderID { get; set; }
	public DateTime OrderCreatedAt { get; set; }
	public string OrderTrackingCode { get; set; } = string.Empty;
	public string CurrentOrderStatus { get; set; } = string.Empty;
	public string CurrentUserName { get; set; } = string.Empty;
	public string CurrentUserSurname { get; set; } = string.Empty;
	public string CurrentUserEmail { get; set; } = string.Empty;
	public string CurrentUserPhoneNumber { get; set; } = string.Empty;
	public decimal SummaryTotal { get; set; }
	public List<OrderItemsDetailsDTO> Order_Item_Details_DTOs { get; set; } = new List<OrderItemsDetailsDTO>();
}
