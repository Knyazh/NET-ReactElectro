using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.DTOs.User;

public class UserLogInDto
{

	[EmailAddress(ErrorMessage = "Invalid email format! Please enter a valid email address...")]

	public string Email { get; set; } = string.Empty;
	[Required(ErrorMessage = "Password field cannot be left blank! Please fill in the relevant field...")]
	
	public string Password { get; set; } = string.Empty;
}
