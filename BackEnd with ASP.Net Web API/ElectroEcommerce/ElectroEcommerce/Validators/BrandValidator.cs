using ElectroEcommerce.DataBase.DTOs.Brand;
using FluentValidation;

namespace ElectroEcommerce.Validators;

public class BrandValidator : AbstractValidator<BrandPostDto>
{
	public BrandValidator ()
		{

		RuleFor(c => c.Name).NotEmpty();
		RuleFor(c => c.Description).NotEmpty();
		RuleFor(c => c.Name).MaximumLength(20).WithMessage("Name can 20 characters maximum ");
		RuleFor(c => c.Description).MaximumLength(50).WithMessage("Description can 50 characters maximum ");
		RuleFor(b=>b.File).NotEmpty();

	}
};
