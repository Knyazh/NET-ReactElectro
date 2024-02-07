using ElectroEcommerce.DataBase.Base;
using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.DTOs.Category;

public class CategoryPostDTO : BaseEntity<Guid>
{
	[Required(ErrorMessage = "Name is required!")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Description is required!")]
	public string Description { get; set; } = string.Empty;
}
