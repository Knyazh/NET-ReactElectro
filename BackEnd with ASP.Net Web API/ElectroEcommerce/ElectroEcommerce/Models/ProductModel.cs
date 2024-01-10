namespace ElectroEcommerce.Models;

public class ProductModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal Price { get; set; } = 0;
	
	public decimal Brand { get; set; }= 0;
	public decimal BrandTotal { get;}= 0;
	public DateTime CreatedDate { get; set; }= DateTime.Now;


}
