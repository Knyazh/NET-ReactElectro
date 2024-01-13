namespace ElectroEcommerce.DataBase.Models;

public class ProductColor
{
	public ProductModel Product { get; set; }
	public Guid ProductId { get; set; }


	public Color Color { get; set; }
	public Guid ColorId { get; set; }
}
