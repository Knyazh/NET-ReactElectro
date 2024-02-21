using ElectroEcommerce.DataBase.DTOs.Product;
using ElectroEcommerce.DataBase.Models;
using FluentValidation;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace ElectroEcommerce.Validators;

public class ProductModelValidator : AbstractValidator<ProductPostDto>
{
	public ProductModelValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("You must add name");
		RuleFor(x => x.Name).MaximumLength(15).WithMessage("You can add 15 character maximum");
		RuleFor(x => x.Description).MaximumLength(50).WithMessage("You can add 50 character maximum");
		RuleFor(x => x.Description).NotEmpty().WithMessage("You must add description");
		RuleFor(x => x.Price).NotEmpty();
		RuleFor(x => x.Quantity).NotEmpty();
		RuleFor(x => x.CurrentBrandId).NotEmpty().WithMessage("You must add BrandId.");
		RuleFor(x => x.CurrentCategoryId).NotEmpty().WithMessage("You must add CategoryId.");
		RuleFor(x => x.ColorIds).NotEmpty().WithMessage("You must add ColorId.");
		RuleFor(x => x.PyshicalImageNames).NotEmpty().WithMessage("You must add Image.");

	}
}
