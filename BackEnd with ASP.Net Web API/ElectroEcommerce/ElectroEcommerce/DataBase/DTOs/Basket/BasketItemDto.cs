namespace ElectroEcommerce.DataBase.DTOs.Basket;

public class BasketItemDto
{
	public Guid ProductId { get; set; }
	public Guid ColorId { get; set; }
	public int Quantity { get; set; }
}
