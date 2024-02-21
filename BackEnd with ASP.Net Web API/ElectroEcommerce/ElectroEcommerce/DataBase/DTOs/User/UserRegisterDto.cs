using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.DTOs.User;

public class UserRegisterDto
{
	[Required(ErrorMessage = "You have to fill this blank")]
	
	public string Name { get; set; } = string.Empty;
	[Required(ErrorMessage = "You have to fill this blank")]

	public string LastName { get; set; } = string.Empty;

	[Required(ErrorMessage = "You have to fill this blank")]

	public string Email { get; set; } = string.Empty;
	[Required(ErrorMessage = "You have to fill this blank")]

	public string Password { get; set; } = string.Empty;
	[Required(ErrorMessage = "Confirm password fill")]
	
	public string ConfirmPassword { get; set; } = string.Empty;
	[Required(ErrorMessage = "Phone number field cannot be left blank! Please fill in the relevant field...")]
	
	public string PhoneNumber { get; set; } = string.Empty;
	[Display(Name = "Image")]
	[Required(ErrorMessage = "Pick an Image!")]
	public IFormFile File { get; set; }
}
