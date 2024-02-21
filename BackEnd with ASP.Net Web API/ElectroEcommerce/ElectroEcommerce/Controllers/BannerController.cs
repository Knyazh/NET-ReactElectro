using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.Product;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Abstracts;
using ElectroEcommerce.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using ElectroEcommerce.DataBase.DTOs.Banner;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers;
[Route("api/banner")]
[ApiController]
public class BannerController:ControllerBase	
{
	private readonly DataContext _dataContext;
	private readonly IFileService _fileService;
	private readonly IVerificationService _verificationService;
	private readonly ILogger<ProductController> _logger;


	public BannerController(DataContext dataContext, IFileService fileService, IVerificationService verificationService, ILogger<ProductController> logger)
	{
		_dataContext = dataContext;
		_fileService = fileService;
		_verificationService = verificationService;
		_logger = logger;
	}


	[HttpPost("add")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> Add([FromForm] BannerPostDto bannerPostDto)
	{
		try
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var banner = new Banner
			{
				Name = bannerPostDto.Name,
				Description = bannerPostDto.Description,
				BannerPrefix = _verificationService.RandomFolderPrefixGenerator(Prefix.BANNER),
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};


			if (bannerPostDto.Files.Count > 0)
			{
				banner.Files = await _fileService
					.UploadAsync(CustomUploadDirectories.Banners, bannerPostDto.Files, banner.BannerPrefix);
			}

			await _dataContext.Banners.AddAsync(banner);
			await _dataContext.SaveChangesAsync();

			var jsonOptions = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.Preserve
			};

			var URL = "https://localhost:7010/api/v1/product/get/" + banner.Id;
			return Created(URL, JsonSerializer.Serialize(banner, jsonOptions));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");
			return StatusCode(500, ex.Message);
		}
	}




	[HttpGet("get-all")]
	[Produces(typeof(List<BannerListItemDto>))]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get()
	{
		try
		{
			var banner = await _dataContext.Banners.Select(b => new BannerListItemDto
			{
				Id = b.Id,
				Name = b.Name,
				Description = b.Description,
				Files = _fileService.ReadStaticFiles(b.BannerPrefix, CustomUploadDirectories.Banners, b.Files),
				CreatedAt = b.CreatedAt,
				UpdatedAt = b.UpdatedAt,
				BannerPrefix = b.BannerPrefix,

			}).ToListAsync();
			return Ok(banner);

		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}

	}


	[HttpGet("get/{Id}")]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	[Produces(type: typeof(BannerListItemDto))]
	public async Task<IActionResult> Get([FromRoute] Guid Id)
	{
		try
		{
			var banner = await _dataContext.Banners.SingleOrDefaultAsync(br => br.Id.Equals(Id));
			if (banner is null)
			{
				return NotFound($"The banner the << {Id} >> not database yet ");
			}

			var response = new BannerListItemDto
			{
				Id = banner.Id,
				Name = banner.Name,
				Description = banner.Description,
				Files = _fileService.ReadStaticFiles(banner.BannerPrefix, CustomUploadDirectories.Banners, banner.Files),
				BannerPrefix = banner.BannerPrefix,
				CreatedAt = banner.CreatedAt,
				UpdatedAt = banner.UpdatedAt
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
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> Update([FromRoute] Guid Id, [FromForm] BannerPostDto bannerPostDto)
	{
		try
		{
			var banner = await _dataContext.Banners.SingleOrDefaultAsync(br => br.Id.Equals(Id));
			if (banner is null) return NotFound($"The Banner this <<{Id}>> no database yet!");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (bannerPostDto.Files is not null)
			{
				_fileService.RemoveStaticFiles(banner.BannerPrefix, CustomUploadDirectories.Banners, banner.Files);
				banner.Files = await _fileService.UploadAsync(CustomUploadDirectories.Banners, bannerPostDto.Files, banner.BannerPrefix);
			}

			banner.Name = bannerPostDto.Name;
			banner.Description = bannerPostDto.Description;
			banner.UpdatedAt = DateTime.UtcNow;


			_dataContext.Banners.Update(banner);
			await _dataContext.SaveChangesAsync();

			return Ok(banner);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");
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
			var banner = await _dataContext.Banners.SingleOrDefaultAsync(br => br.Id.Equals(Id));
			if (banner is null)
				return NotFound($"The banner this <<{Id}>> no database yet");

			if (banner.Files is not null)
			{
				_fileService.RemoveStaticFiles(banner.BannerPrefix, CustomUploadDirectories.Banners, banner.Files);

			}

			_dataContext.Banners.Remove(banner);
			await _dataContext.SaveChangesAsync();

			return NoContent();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "An error occurred while processing the request.");
			return StatusCode(500, "An error occurred while processing the request. Please try again later.");
		}
	}
}
