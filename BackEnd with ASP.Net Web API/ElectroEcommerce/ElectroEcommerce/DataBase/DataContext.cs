using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.DataBase;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>().HasData(

			new User
			{
				Id = Guid.NewGuid(),
				Name = "Knyaz",
				LastName = "Heydarov",
				Email = "knyazheydariv@gmail.com",
				Password = "password",

			}
			);
	}



	public DbSet<User> Users { get; set; }

	public DbSet<ProductModel> Products { get; set; }

	public DbSet<Category> Categories { get; set; }


}
