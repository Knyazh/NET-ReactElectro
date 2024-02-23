namespace ElectroEcommerce.DataBase.DTOs.Basket;

public class BasketListItemDTO
{
	public string ProductName { get; set; } = string.Empty;
	public string[] PhisicalimageURLs { get; set; } = new string[] { };
	public decimal Price { get; set; }
	public bool IsAviable { get; set; }
	public int Quantity { get; set; }
	public Guid ProductId { get; set; }
	public Guid ColorID { get; set; }
}
