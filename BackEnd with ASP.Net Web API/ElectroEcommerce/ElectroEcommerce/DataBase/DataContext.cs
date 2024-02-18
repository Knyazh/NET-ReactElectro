using ElectroEcommerce.DataBase.Base;
using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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

		base.OnModelCreating(modelBuilder);

		#region 1x Many Email and User

		modelBuilder.Entity<Email>()
			.HasOne<User>(e => e.User)
			.WithMany(u => u.Emails)
			.HasForeignKey(e => e.UserId);
		#endregion

		#region 1x 1 User and Activationtoken
		modelBuilder.Entity<User>()
			  .HasOne<ActivationToken>(u => u.ActivationToken)
			  .WithOne(act => act.User)
			  .HasForeignKey<ActivationToken>(act => act.UserId);

		#endregion

		#region 1x Many Brand and Product
		modelBuilder.Entity<ProductModel>()
			 .HasOne<Brand>(p => p.Brand)
			 .WithMany(br => br.Products)
			 .HasForeignKey(p => p.CurrentBrandId);
		#endregion

		#region 1x Many User and Order
		modelBuilder.Entity<Order>()
			.HasOne<User>(o => o.User)
			.WithMany(u => u.Orders)
			.HasForeignKey(o => o.UserId);

		#endregion

		#region  many to many Product and Color
		modelBuilder.Entity<ProductColor>()
			 .HasKey(pc => new { pc.ProductId, pc.ColorId });

		modelBuilder.Entity<ProductColor>()
			.HasOne(pc => pc.Product)
			.WithMany(p => p.ProductColors)
			.HasForeignKey(pc => pc.ProductId);

		modelBuilder.Entity<ProductColor>()
			.HasOne(pc => pc.Color)
			.WithMany(c => c.ProductColors)
			.HasForeignKey(pc => pc.ColorId);
		#endregion


		#region Admin Seeding
		modelBuilder.Entity<User>().HasData(
			new User
			{
				Id = Guid.NewGuid(),
				Name = "Knyaz",
				LastName = "Heydarov",
				Email = "knyazheydariv@gmail.com",
				Password = "Knyaz123.",
				Role = Contracts.Role.Values.SuperAdmin,
				IsAdmin = true
			}
			);
		#endregion



		#region Color seeding...
		modelBuilder.Entity<Color>().HasData
		(
			new Color
			{
				Id = new Guid("4b24804b-9a8f-4d33-9f43-8c461e4dbf11"),
				ColorCode = "#000000",
				Name = "black",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("9c327764-bf90-4b8b-8c38-370cb3aa2a5a"),
				ColorCode = "#ffffff",
				Name = "white",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("8a4c94f8-2437-4e89-9075-56bbcf19c0e9"),
				ColorCode = "#a52a2a",
				Name = "brown",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("af15118a-95ac-487a-b103-c9a0a1918c25"),
				ColorCode = "#0000ff",
				Name = "blue",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("6c1d50fc-b6eb-4d76-ba7e-81a7811ea15f"),
				ColorCode = "#ff0000",
				Name = "red",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("3e20ac3a-c156-4f60-b0b4-e1f1c205e24d"),
				ColorCode = "#808080",
				Name = "gray",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("db1ef4d9-5b95-41a3-8bfb-7f01f8a50f32"),
				ColorCode = "#008000",
				Name = "green",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("45e0cbf9-aa2a-44a4-93a1-bf4d3aa623ce"),
				ColorCode	 = "#ffff00",
				Name = "yellow",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("8640c057-8997-4b16-b3dd-7c3d3c2e1a12"),
				ColorCode = "#2f4f4f",
				Name = "darkslategray",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("7a9d04e8-1a22-4aae-8232-62f5a0c28b87"),
				ColorCode = "#663399",
				Name = "rebeccapurple",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("2ff83d6b-6c95-4f7d-9c64-60e406a057a1"),
				ColorCode = "#ffe4c4",
				Name = "bisque",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("9a305d7f-5c8f-4fe1-9c0d-d8a8eb4a17c3"),
				ColorCode = "#ffe4c4",
				Name = "bisque",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
			},
			new Color
			{
				Id = new Guid("c80a742d-12db-4a19-a0e8-44c67f7fb21a"),
				ColorCode = "#00ced1",
				Name = "darkturquoise",
				CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
				UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
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
	public DbSet<ActivationToken> ActivationTokens { get; set; }
	public DbSet<Email> Emails { get; set; }
	public DbSet<Brand> Brands { get; set; }




}
