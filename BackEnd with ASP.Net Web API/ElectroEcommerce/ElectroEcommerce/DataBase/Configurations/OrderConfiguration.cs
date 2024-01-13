using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectroEcommerce.DataBase.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{

		builder
			.HasOne<User>(o => o.User)
			.WithMany(u => u.Orders)
			.HasForeignKey(o => o.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
