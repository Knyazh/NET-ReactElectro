﻿using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.DataBase.DTOs.User;

public class UserUpdateDto
{
	[Required(ErrorMessage = "You have to fill this blank")]
	[RegularExpression(pattern: @"^[a-zA-Z]+$", ErrorMessage = "Name must consist of only uppercase and lowercase letters!")]
	public string Name { get; set; } = string.Empty;
	[Required(ErrorMessage = "You have to fill this blank")]
	[RegularExpression(pattern: @"^[a-zA-Z]+$", ErrorMessage = "Last name must consist of only uppercase and lowercase letters!")]
	public string LastName { get; set; } = string.Empty;

	[Required(ErrorMessage = "You have to fill this blank")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be minimum 6 ")]
	[RegularExpression(pattern: @"^[a-zA-Z0-9]+$", ErrorMessage = "The entered password must consist of only uppercase, lowercase letters and numbers!")]
	public string Password { get; set; } = string.Empty;
	[Required(ErrorMessage = "Confirm password fill")]
	[Compare("Password", ErrorMessage = "Password's doesn't match")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be minimum 6")]
	[RegularExpression(pattern: @"^[a-zA-Z0-9]+$", ErrorMessage = "The entered confirm password must consist of only uppercase, lowercase letters and numbers!")]
	public string ConfirmPassword { get; set; } = string.Empty;

	[Required(ErrorMessage = "Phone number field cannot be left blank! Please fill in the relevant field...")]
	[RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format! Please enter a valid phone number...")]
	[StringLength(13, ErrorMessage = "Phone number must be at most 13 characters long!")]
	[Phone(ErrorMessage = "Please enter a valid phone number...")]
	public string PhoneNumber { get; set; } = string.Empty;

	[Display(Name = "Image")]
	[Required(ErrorMessage = "Pick an Image!")]
	public IFormFile File { get; set; }

}
