using ElectroEcommerce.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ElectroEcommerce.DataBase;

public class SeedData
{
	public static void SeedDatabase(DataContext context)
	{
		context.Database.Migrate();

		#region Seed Colors
		if (!context.Colors.Any())
		{


			context.Colors.AddRange(

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
					ColorCode = "#ffff00",
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
			context.SaveChanges();

		}
		#endregion

		#region Seed Electronics Categories
		if (!context.Categories.Any())
		{
			context.Categories.AddRange(
				new Category
				{
					Id = new Guid("6f8de930-314e-4e77-8f9c-5d03e4312d80"),
					Name = "Laptops",
					Description = "Portable computers",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("5f4a0b21-2e4c-4e92-9a64-7b7b1aebb1e2"),
					Name = "Smartphones",
					Description = "Mobile phones with advanced features",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("aedbbd0c-2d8d-4cd4-9982-2799d5a1831d"),
					Name = "Tablets",
					Description = "Portable touchscreen computers",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("bbf36e40-86c6-4e08-aeb1-06d1e320624b"),
					Name = "Desktop Computers",
					Description = "Non-portable computers",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("7d54dcbf-9bfb-40cd-86f7-6ec5fc1c1f5d"),
					Name = "Monitors",
					Description = "Computer display screens",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("aa536c53-98e3-4d24-87c0-647c3eeb8e4f"),
					Name = "Printers",
					Description = "Devices that produce hard copies of documents",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("92dcfc2c-b4a3-4ab6-96e8-5e2c7346b734"),
					Name = "Networking Devices",
					Description = "Devices used to connect computers in a network",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("59f1561b-6c1b-43f6-8bb5-5a28a50d3937"),
					Name = "Storage Devices",
					Description = "Devices used to store data",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				},
				new Category
				{
					Id = new Guid("04c2ba46-d0f4-4e41-ae6e-61e11b888d42"),
					Name = "Accessories",
					Description = "Additional items for electronic devices",
					CreatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc),
					UpdatedAt = new DateTime(2023, 10, 19, 0, 0, 0, DateTimeKind.Utc)
				}
			);
			context.SaveChanges();
		}
		#endregion

	}
}
