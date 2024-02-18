using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Abstracts;
using ElectroEcommerce.Services.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using ElectroEcommerce.DataBase.DTOs.Brand;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers
{
	[Route("api/brands")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly DataContext _dataContext;
		private readonly IVerificationService _verificationService;
		private readonly IFileService _fileService;
		private readonly ILogger<BrandController> _logger;
		private readonly IEmailService _emailService;

		public BrandController(DataContext dataContext, IVerificationService verificationService, 
			IFileService fileService, ILogger<BrandController> logger, IEmailService emailService)
		{
			_dataContext = dataContext;
			_verificationService = verificationService;
			_fileService = fileService;
			_logger = logger;
			_emailService = emailService;
		}


		[HttpPost("post")]
		[ProducesResponseType(statusCode: StatusCodes.Status201Created)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Post([FromForm] BrandPostDto brandPostDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var brand = new Brand
			{
				Name = brandPostDto.Name,
				Description = brandPostDto.Description,
				BrandPrefix = _verificationService.RandomFolderPrefixGenerator(Prefix.BRAND),
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			brand.LogoURL = await _fileService
				.UploadAsync(CustomUploadDirectories.Brands, brandPostDto.File, brand.BrandPrefix);

			try
			{
				await _dataContext.Brands.AddAsync(brand);
				await _dataContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Processing error.");

				return StatusCode(500,ex.Message );
			}

			var jsonOptions = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };

			var URL = "https://localhost:7069/api/brands/get/" + brand.Id;
			return Created(URL, JsonSerializer.Serialize(brand, jsonOptions));
		}



		[HttpGet("get/{Id}")]
		[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		[Produces(type: typeof(BrandListItemDto))]
		public async Task<IActionResult> Get([FromRoute] Guid Id)
		{
			try
			{
				var brand = await _dataContext.Brands.SingleOrDefaultAsync(br => br.Id.Equals(Id));
				if (brand is null)
				{
					return NotFound($"The brand the << {Id} >> not database yet ");
				}

				var response = new BrandListItemDto
				{
					Id = brand.Id,
					Name = brand.Name,
					Description = brand.Description,
					LogoUrl = _fileService.ReadStaticFiles(brand.BrandPrefix, CustomUploadDirectories.Brands, brand.LogoURL),
					BrandPrefix = brand.BrandPrefix,
					CreatedAt = brand.CreatedAt,
					UpdatedAt = brand.UpdatedAt
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Processing error");

				return StatusCode(500,ex.Message);
			}

		}



		[HttpGet("get-all")]
		[Produces(type: typeof(List<BrandListItemDto>))]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Get()
		{
			try
			{
				var responses = await _dataContext.Brands.Select(br => new BrandListItemDto
				{
					Id = br.Id,
					Name = br.Name,
					Description = br.Description,
					BrandPrefix = br.BrandPrefix,
					LogoUrl = _fileService.ReadStaticFiles(br.BrandPrefix, CustomUploadDirectories.Brands, br.LogoURL),
					CreatedAt = br.CreatedAt,
					UpdatedAt = br.UpdatedAt
				}).ToListAsync();

				return Ok(responses);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Processing error.");
				return StatusCode(500, ex.Message);
			}
		}



		[HttpDelete("delete/{Id}")]
		[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete([FromRoute] Guid Id)
		{
			try
			{
				var brand = await _dataContext.Brands.SingleOrDefaultAsync(br => br.Id.Equals(Id));
				if (brand is null)
					return NotFound($"The brand this <<{Id}>> no database yet");

				if (brand.LogoURL is not null)
				{
					_fileService.RemoveStaticFiles(brand.BrandPrefix, CustomUploadDirectories.Brands, brand.LogoURL);

				}

				_dataContext.Brands.Remove(brand);
				await _dataContext.SaveChangesAsync();

				return NoContent();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "An error occurred while processing the request.");
				return StatusCode(500, "An error occurred while processing the request. Please try again later.");
			}
		}
		[HttpGet("search")]
		[Produces(type: typeof(List<BrandListItemDto>))]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		public async Task<IActionResult> Search([FromQuery(Name = "query")] string query)
		{
		
			try
			{
				var brands = await _dataContext.Brands.ToListAsync();

				if (brands.Count == 0)
					return Ok(new List<BrandListItemDto>());

				var responses = brands.Where(br => br.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
					.Select(br => new BrandListItemDto
					{
						Id = br.Id,
						Name = br.Name,
						Description = br.Description,
						BrandPrefix = br.BrandPrefix,
						LogoUrl = _fileService.ReadStaticFiles(br.BrandPrefix, CustomUploadDirectories.Brands, br.LogoURL),
						CreatedAt = br.CreatedAt,
						UpdatedAt = br.UpdatedAt

					}).ToList();

				return Ok(responses);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Processing error");
				return StatusCode(500, ex.Message);
			}
		}


		[HttpPut("update/{Id}")]
		[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
		[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Update([FromRoute] Guid Id, [FromForm] BrandPostDto brandPostDto)
		{
			try
			{
				var brand = await _dataContext.Brands.SingleOrDefaultAsync(br => br.Id.Equals(Id));
				if (brand is null) return NotFound($"The brand this <<{Id}>> no database yet!");

				if (!ModelState.IsValid) return BadRequest(ModelState);

				if (brandPostDto.File is not null)
				{
					_fileService.RemoveStaticFiles(brand.BrandPrefix, CustomUploadDirectories.Brands, brand.LogoURL);
					brand.LogoURL = await _fileService.UploadAsync(CustomUploadDirectories.Brands, brandPostDto.File, brand.BrandPrefix);
				}

				brand.Name = brandPostDto.Name;
				brand.Description = brandPostDto.Description;
				brand.UpdatedAt = DateTime.UtcNow;


				_dataContext.Brands.Update(brand);
				await _dataContext.SaveChangesAsync();

				return Ok(brand);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Processing error");
				return StatusCode(500, ex.Message);
			}
		}
	}
}
