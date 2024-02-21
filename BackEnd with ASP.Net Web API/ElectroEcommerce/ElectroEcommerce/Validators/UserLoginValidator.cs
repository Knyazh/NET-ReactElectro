using ElectroEcommerce.DataBase.DTOs.User;
using FluentValidation;

namespace ElectroEcommerce.Validators;

public class UserLoginValidator : AbstractValidator<UserLogInDto>
{
	public UserLoginValidator()
	{
		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("You have to fill this blank.")
			.MinimumLength(6).WithMessage("Password length must be minimum 6.")
			.Matches("^[a-zA-Z0-9]+$").WithMessage("The entered password must consist of only uppercase, lowercase letters, and numbers!");
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("You have to fill this blank.")
			.EmailAddress().WithMessage("Invalid email format.");
	}
}
