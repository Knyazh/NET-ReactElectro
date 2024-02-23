using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.Product;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Concretes;

namespace ElectroEcommerce.Controllers;

[ApiController]
[Route("api/v1/product")]
public class ProductController : ControllerBase
{
	private readonly DataContext _dataContext;
	private readonly IFileService _fileService;
	private readonly IVerificationService _verificationService;
	private readonly ILogger<ProductController> _logger;


	public ProductController(DataContext dataContext, IFileService fileService, IVerificationService verificationService, ILogger<ProductController> logger)
	{
		_dataContext = dataContext;
		_fileService = fileService;
		_verificationService = verificationService;
		_logger = logger;
	}


	[HttpGet("get-all")]
	[Produces(typeof(List<ProductListItemDto>))]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get()
	{
		try
		{
			var product = await _dataContext.Products.Select(p => new ProductListItemDto
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Price= p.Price,
				Quantity= p.Quantity,
				IsAvailable= p.IsAvailable,
				PhisicalImageNames = _fileService.ReadStaticFiles(p.ProductPrefix, CustomUploadDirectories.Products, p.PyshicalImageNames.ToList()),
				CreatedAt = p.CreatedAt,
				UpdatedAt = p.UpdatedAt,
				ProductPrefix = p.ProductPrefix,
				Brand = _dataContext.Brands
					.SingleOrDefault(br => br.Id.Equals(p.CurrentBrandId))!,
				Category = _dataContext.Categories
					.SingleOrDefault(c => c.Id.Equals(p.CurrentCategoryId))!,

				Colors = _dataContext.ProductColors
					.Where(pc => pc.ProductId.Equals(p.Id))
					.Select(pc => pc.Color).ToList()

			}).ToListAsync();
			return Ok(product);

		}
		catch (Exception ex)
		{
			return StatusCode(500,ex.Message);
		}

	}


	[HttpPost("add")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> Add([FromForm] ProductPostDto productPostDto)
	{
		try
		{

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var product = new ProductModel
		{
			Name = productPostDto.Name,
			Description = productPostDto.Description,
			Price = productPostDto.Price,
			ProductPrefix = _verificationService.RandomFolderPrefixGenerator(Prefix.CATEGORY),
			Quantity = productPostDto.Quantity,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};

			if ((await _dataContext.Brands.AnyAsync(br => br.Id.Equals(productPostDto.CurrentBrandId))) is false)
			{
				return NotFound($"The brand with the  {productPostDto.CurrentBrandId}  Does not exist yet ");
			}


			product.CurrentBrandId = productPostDto.CurrentBrandId;

			if ((await _dataContext.Categories.AnyAsync(c => c.Id.Equals(productPostDto.CurrentCategoryId))) is false)
			{
				return NotFound($"The brand with the  {productPostDto.CurrentCategoryId}  Does not exist yet ");
			}


			product.CurrentCategoryId = productPostDto.CurrentCategoryId;


			if (productPostDto.PyshicalImageNames.Count > 0)
		{
			product.PyshicalImageNames = (await _fileService
				.UploadAsync(CustomUploadDirectories.Products, productPostDto.PyshicalImageNames, product.ProductPrefix)).ToArray();
		}

		 await _dataContext.Products.AddAsync(product);


		if (productPostDto.ColorIds.Length > 0)
		{
			foreach (var ColorId in productPostDto.ColorIds)
			{
				var color = await _dataContext.Colors.SingleOrDefaultAsync(c => c.Id.Equals(ColorId));
				if (color is null)
					return NotFound($"The color with the  {ColorId}   Does not exist yet");

				var product_color = new ProductColor
				{
					ColorId = ColorId,
					Product = product
				};

				await _dataContext.ProductColors.AddAsync(product_color);
			}
		}

			await _dataContext.SaveChangesAsync();

			var jsonOptions = new JsonSerializerOptions
		{
			ReferenceHandler = ReferenceHandler.Preserve
		};

		var URL = "https://localhost:7010/api/v1/product/get/" + product.Id;
		return Created(URL, JsonSerializer.Serialize(product, jsonOptions));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("get/{Id}")]
	[Produces(type: typeof(ProductListItemDto))]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get([FromRoute] Guid Id)
	{
		try
		{
			var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(Id));

			if (product is null)
				return NotFound($"The product with the < {Id} > number not found");

			var response = new ProductListItemDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				ProductPrefix = product.ProductPrefix,
				Quantity = product.Quantity,
				IsAvailable = product.IsAvailable,
				CreatedAt = product.CreatedAt,
				UpdatedAt = product.UpdatedAt,
				PhisicalImageNames = _fileService
				.ReadStaticFiles(product.ProductPrefix, CustomUploadDirectories.Products, product.PyshicalImageNames.ToList()),

				Brand = _dataContext.Brands
				.SingleOrDefault(br => br.Id.Equals(product.CurrentBrandId))!,
				Category = _dataContext.Categories
				.SingleOrDefault(c => c.Id.Equals(product.CurrentCategoryId))!,

				Colors = await _dataContext.ProductColors
				.Where(pc => pc.ProductId.Equals(product.Id))
				.Select(pc => pc.Color).ToListAsync()
			};

			return Ok(response);

		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");

			return StatusCode(500, ex.Message);
		}
	}


	[HttpPut("update/{Id}")]
	[Produces(typeof(ProductModel))]
	[Consumes("multipart/form-data")]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Update([FromRoute] Guid Id, [FromForm] ProductPostDto productPostDto)
	{
		try
		{
			var product = await _dataContext.Products.SingleOrDefaultAsync(pr => pr.Id.Equals(Id));
			if (product is null)
				return NotFound($"The product with the << {Id} >>  Does not exist yet");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			product.Name = productPostDto.Name;
			product.Description = productPostDto.Description;
			product.Price = productPostDto.Price;
			product.Quantity = productPostDto.Quantity;
			product.UpdatedAt = DateTime.UtcNow;

		

			if ((await _dataContext.Brands.AnyAsync(br => br.Id.Equals(productPostDto.CurrentBrandId))) is false)
			{
				return NotFound($"The brand with the {productPostDto.CurrentBrandId}   Does not exist yet");
			}

			product.CurrentBrandId = productPostDto.CurrentBrandId;

			if (productPostDto.PyshicalImageNames.Count > 0)
			{
				_fileService
					.RemoveStaticFiles(product.ProductPrefix,CustomUploadDirectories.Products,product.PyshicalImageNames.ToList());

				product.PyshicalImageNames = (await _fileService
					.UploadAsync(CustomUploadDirectories.Products,
					productPostDto.PyshicalImageNames, product.ProductPrefix)).ToArray();
			}

			_dataContext.Products.Update(product);

			if (productPostDto.ColorIds.Length > 0)
			{
				var removeableProductColors = await _dataContext.ProductColors
					.Where(pc => pc.ProductId.Equals(product.Id)).ToListAsync();

				foreach (var colorId in productPostDto.ColorIds)
				{
					var color = await _dataContext.Colors.SingleOrDefaultAsync(c => c.Id.Equals(colorId));
					if (color is null)
					{
						return NotFound($"The color with the  {colorId}   Does not exist yet");
					}
					var removableProductColor = removeableProductColors.SingleOrDefault(pc => pc.ColorId.Equals(colorId));
					if (removableProductColor is null)
					{
						var productColor = new ProductColor
						{
							ProductId = product.Id,
							ColorId = colorId,
						};
						await _dataContext.ProductColors.AddAsync(productColor);
					}
					else
					{
						removeableProductColors.Remove(removableProductColor);
					}
				}

				_dataContext.ProductColors.RemoveRange(removeableProductColors);
			}
		
			await _dataContext.SaveChangesAsync();
			return Ok(product);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");

			return StatusCode(500, ex.Message);
		}
	}


	[HttpDelete("delete/{Id}")]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid Id)
	{
		try
		{
			var product = await _dataContext.Products.SingleOrDefaultAsync(p=>p.Id.Equals(Id));

			if (product == null)
			{
				return NotFound($"The product with the id {Id} was not found.");
			}

			if (product.PyshicalImageNames.ToList().Count > 0)
			{
				_fileService
				   .RemoveStaticFiles(product.ProductPrefix, CustomUploadDirectories.Products, product.PyshicalImageNames.ToList());
			}


			var removeableProductColors = await _dataContext.ProductColors
				   .Where(pc => pc.ProductId.Equals(product.Id)).ToListAsync();
			if (removeableProductColors.Count > 0)
			{
				_dataContext.ProductColors.RemoveRange(removeableProductColors);
			}

			_dataContext.Products.Remove(product);
			await _dataContext.SaveChangesAsync();

			return Ok($"Product with id {Id} was successfully deleted.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error deleting product");

			return StatusCode(500, ex.Message);
		}
	}



	[HttpGet("get-limited")]
	[Produces(typeof(List<ProductListItemDto>))]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get([FromQuery] int limit)
	{
		try
		{
			var products = await _dataContext.Products
				.Take(limit)
				.Select(p => new ProductListItemDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Price = p.Price,
					Quantity = p.Quantity,
					IsAvailable = p.IsAvailable,
					PhisicalImageNames = _fileService.ReadStaticFiles(p.ProductPrefix, CustomUploadDirectories.Products, p.PyshicalImageNames.ToList()),
					CreatedAt = p.CreatedAt,
					UpdatedAt = p.UpdatedAt,
					ProductPrefix = p.ProductPrefix,
					Brand = _dataContext.Brands
						.SingleOrDefault(br => br.Id.Equals(p.CurrentBrandId))!,
					Category = _dataContext.Categories
						.SingleOrDefault(c => c.Id.Equals(p.CurrentCategoryId))!,

					Colors = _dataContext.ProductColors
						.Where(pc => pc.ProductId.Equals(p.Id))
						.Select(pc => pc.Color).ToList()
				})
				.ToListAsync();
			return Ok(products);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}



}
