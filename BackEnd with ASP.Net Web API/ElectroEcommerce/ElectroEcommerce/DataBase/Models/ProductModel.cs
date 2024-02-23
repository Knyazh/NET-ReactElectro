using ElectroEcommerce.DataBase.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroEcommerce.DataBase.Models;

public class ProductModel : BaseEntity<Guid>, IAuditable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;

	private decimal _quantity;
	private bool _isAvailable = true;

	public decimal Quantity
	{
		get { return _quantity; }
		set
		{
			_quantity = value;

			_isAvailable = (_quantity > 0);
		}
	}

	public bool IsAvailable
	{
		get { return _isAvailable; }
		private set { }
	}

	public string[] PyshicalImageNames { get; set; } = new string[] { };
	public string ProductPrefix { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public Guid CurrentBrandId { get; set; }
	public Guid? CurrentCategoryId { get; set; }

	public List<ProductColor> ProductColors { get; set; }

	public Category? Category { get; set; }
	public Brand Brand { get; set; }
}
