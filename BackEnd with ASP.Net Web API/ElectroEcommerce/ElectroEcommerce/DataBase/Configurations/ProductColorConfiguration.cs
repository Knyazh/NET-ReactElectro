using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectroEcommerce.DataBase.Configurations;

public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
{
	public void Configure(EntityTypeBuilder<ProductColor> builder)
	{
		builder.HasKey(pc=> new {pc.ProductId,pc.ColorId});

		builder
			 .HasOne<ProductModel>(pc => pc.Product)
			 .WithMany(p => p.ProductColors)
			 .HasForeignKey(pc => pc.ProductId);

		builder
			.HasOne<Color>(pc => pc.Color)
		   .WithMany(p => p.ProductColors)
		   .HasForeignKey(pc => pc.ColorId);
	}
}
