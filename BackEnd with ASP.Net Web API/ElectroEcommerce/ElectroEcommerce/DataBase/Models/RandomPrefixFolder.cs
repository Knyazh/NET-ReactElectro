using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.Models;

public class RandomPrefixFolder
{
	[Key]
	public string RandomPrefix { get; set; } =string.Empty;
}
