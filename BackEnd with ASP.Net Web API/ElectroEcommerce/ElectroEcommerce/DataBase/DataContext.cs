using ElectroEcommerce.DataBase.Base;
using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.DataBase;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }


	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries())
		{

			if (entry.Entity is not IAuditable)
				continue;

			IAuditable auditable = entry.Entity as IAuditable;
			if(entry.State == EntityState.Added)
			{
				auditable.CreatedAt = DateTime.UtcNow;
				auditable.UpdatedAt = DateTime.UtcNow;
			}
			else if (entry.State == EntityState.Modified)
			{
				auditable.UpdatedAt = DateTime.UtcNow;
			}

		}

		return await base.SaveChangesAsync(cancellationToken);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		//modelBuilder.ApplyConfigurationsFromAssembly(typeof (DataContext).Assembly);


		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>().HasData(



		#region Admin Seeding
			new User
			{
				Id = Guid.NewGuid(),
				Name = "Knyaz",
				LastName = "Heydarov",
				Email = "knyazheydariv@gmail.com",
				Password = "password",
				Role = Contracts.Role.Values.SuperAdmin
			}
			);
		#endregion
	}




	public DbSet<User> Users { get; set; }

	public DbSet<ProductModel> Products { get; set; }

	public DbSet<Category> Categories { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Color> Colors{ get; set; }
	public DbSet<ProductColor> ProductColors { get; set; }
	public DbSet<RandomPrefixFolder> PrefixFolders { get; set; }

	public DbSet<MyOrder> MyOrders { get; set; }



}
