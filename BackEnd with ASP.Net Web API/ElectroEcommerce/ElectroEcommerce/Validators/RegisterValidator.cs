using ElectroEcommerce.DataBase.DTOs.User;
using FluentValidation;

namespace ElectroEcommerce.Validators;

public class RegisterValidator : AbstractValidator<UserRegisterDto>
{
	public RegisterValidator()
	{
		RuleFor(x => x.Name)
		.NotNull().WithMessage("Name must not be null.")
		.NotEmpty().WithMessage("You have to fill this blank.")
		.Matches("^[a-zA-Z]+$").WithMessage("Name must consist of only uppercase and lowercase letters.");

		RuleFor(x => x.LastName)
		   .NotEmpty().WithMessage("You have to fill this blank.")
		   .Matches("^[a-zA-Z]+$").WithMessage("Last name must consist of only uppercase and lowercase letters!");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("You have to fill this blank.")
			 .EmailAddress().WithMessage("Invalid email format.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("You have to fill this blank.")
			.MinimumLength(6).WithMessage("Password length must be minimum 6.")
			.Matches("^[a-zA-Z0-9]+$").WithMessage("The entered password must consist of only uppercase, lowercase letters, and numbers!");

		RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage("Confirm password fill.")
			.Equal(x => x.Password).WithMessage("Passwords don't match.")
			.Matches("^[a-zA-Z0-9]+$").WithMessage("The entered confirm password must consist of only uppercase, lowercase letters, and numbers!");

		RuleFor(x => x.PhoneNumber)
			.NotEmpty().WithMessage("Phone number field cannot be left blank. Please fill in the relevant field.")
			.Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Invalid phone number format. Please enter a valid phone number.")
			.MaximumLength(13).WithMessage("Phone number must be at most 13 characters long.")
			.Must(BeAValidPhoneNumber).WithMessage("Please enter a valid phone number.");

		RuleFor(x => x.File)
			.NotEmpty().WithMessage("Pick an Image!");
	}
	private bool BeAValidPhoneNumber(string phoneNumber)
	{
		return phoneNumber != null && phoneNumber.Length <= 13;
	}
}
