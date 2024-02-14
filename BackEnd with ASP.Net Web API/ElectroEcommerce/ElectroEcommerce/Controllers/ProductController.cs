using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.DataBase;
using Microsoft.AspNetCore.Mvc;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using ElectroEcommerce.DataBase.DTOs.Category;
using ElectroEcommerce.DataBase.DTOs.Product;
using ElectroEcommerce.Contracts;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ElectroEcommerce.Controllers
{
	[ApiController]
	[Route("api/v1/product")]
	public class ProductController : ControllerBase
	{
		private readonly DataContext _dataContext;
		private readonly IFileService _fileService;
		private readonly IVerificationService _verificationService;


		public ProductController(DataContext dataContext, IFileService fileService, IVerificationService verificationService)
		{
			_dataContext = dataContext;
			_fileService = fileService;
			_verificationService = verificationService;
		}




		[HttpGet("get-all")]
		[Produces(typeof(List<ProductListItemDto>))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<ProductModel>>> Get()
		{
			try
			{
				var product = await _dataContext.Products.Select(p => new ProductListItemDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					PhisicalImageName = _fileService.ReadStaticFiles(p.ProductPrefix, CustomUploadDirectories.Products, p.PyshicalImageName),
					CreatedAt = p.CreatedAt,
					UpdatedAt = p.UpdatedAt,
					ProductPrefix= p.ProductPrefix
				}).ToListAsync();
				return Ok(product);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

		}


		[HttpPost("add-product")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Consumes("multipart/form-data")]
		public async Task<ActionResult<Category>> AddCategory( [FromForm] ProductPostDto productPostDto)
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
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			product.PyshicalImageName = await _fileService.UploadAsync(CustomUploadDirectories.Products, productPostDto.PyshicalImageName,product.ProductPrefix);

			try
			{

			await _dataContext.AddAsync(product);
			await _dataContext.SaveChangesAsync();

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + ex.StackTrace, ex);
			}

			var jsonOptions = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var URL = "https://localhost:7010/api/v1/product/get/" + product.Id;
			return Created(URL, JsonSerializer.Serialize(product, jsonOptions));
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
