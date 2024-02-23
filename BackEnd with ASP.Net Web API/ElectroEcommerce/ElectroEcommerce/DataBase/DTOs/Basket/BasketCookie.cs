namespace ElectroEcommerce.DataBase.DTOs.Basket;

public class BasketCookie
{
	public Guid CurrentUserId { get; set; }
	public Guid ProductID { get; set; }
	public string ProductName { get; set; } = string.Empty;
	public string[] PhisicalImageNames { get; set; } = new string[] { };
	public string ProductPrefix { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	public bool IsAviable { get; set; }
	public Guid ColorID { get; set; }

}
