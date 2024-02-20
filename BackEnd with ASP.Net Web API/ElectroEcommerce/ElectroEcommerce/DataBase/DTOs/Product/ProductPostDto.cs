using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.DTOs.Product
{
	public class ProductPostDto 
	{

		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; } = 0;
		public int Quantity { get; set; }
		public Guid CurrentBrandId { get; set; }
		public Guid CurrentCategoryId { get; set; }
		public Guid[] ColorIds { get; set; }
		public IFormFileCollection PyshicalImageNames { get; set; }
		
	}
}
