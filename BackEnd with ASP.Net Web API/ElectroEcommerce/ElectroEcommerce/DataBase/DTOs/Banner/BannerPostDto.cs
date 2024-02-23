using System.Xml;

namespace ElectroEcommerce.DataBase.DTOs.Banner;

public class BannerPostDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public IFormFile File { get; set; }
}
