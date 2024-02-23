using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.DTOs.Basket;

public class BasketItemDto
{
	[Required]
	public Guid ProductId { get; set; }
	[Required]
	public Guid ColorId { get; set; }
	[Required]
	public int Quantity { get; set; }

}
