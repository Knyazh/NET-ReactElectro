using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;

using System.Text.Json;
using ElectroEcommerce.DataBase.DTOs.Basket;

namespace ElectroEcommerce.Controllers;

[ApiController]
[Route("api/v1/basket")]
public class BasketController : ControllerBase
{
	private readonly DataContext _dataContext;
	private readonly IUserService _userServcie;
	private readonly IBasketService _basketService;
	private readonly ILogger<BasketController> _logger;

	public BasketController(DataContext dataContext, IUserService userServcie, IBasketService basketService, ILogger<BasketController> logger)
	{
		_dataContext = dataContext;
		_userServcie = userServcie;
		_basketService = basketService;
		_logger = logger;
	}

	[HttpPost(template: "add-to-cart")]
	public async Task<IActionResult> Post([FromForm] BasketItemDto DTO)
	{
		if (!ModelState.IsValid)
		{
			ModelState.Clear();

			return BadRequest(ModelState);
		}

		BasketCookie cookieItem = new BasketCookie();

		var product = await _dataContext.Products.SingleOrDefaultAsync(pr => pr.Id.Equals(DTO.ProductId));
		if (product is null)
		{
			ModelState.Clear();
			return BadRequest(ModelState);
		}

		if (product.Quantity < DTO.Quantity)
		{
			ModelState.Clear();
			return BadRequest(ModelState);
		}

		cookieItem.ProductID = product.Id;
		cookieItem.ProductPrefix = product.ProductPrefix;
		cookieItem.Quantity = DTO.Quantity;
		cookieItem.IsAviable = product.IsAvailable;
		cookieItem.CurrentUserId = _userServcie.CurrentUser.Id;
		cookieItem.Price = product.Price * DTO.Quantity;
		cookieItem.ProductName = product.Name;
		cookieItem.PhisicalImageNames = product.PyshicalImageNames;

		var color = await _dataContext.ProductColors
			.Where(pc => pc.ColorId.Equals(DTO.ColorId) && pc.ProductId.Equals(product.Id))
			.Select(pc => pc.Color).SingleOrDefaultAsync();
		if (color is null)
		{
			ModelState.Clear();

			return BadRequest(ModelState);
		}


		cookieItem.ColorID = color.Id;

		var basket_item = _basketService.AppendProductToBasket(cookieItem);

		await _dataContext.BasketItems.AddAsync(basket_item);
		await _dataContext.SaveChangesAsync();

		var jsonOptions = new JsonSerializerOptions
		{
			ReferenceHandler = ReferenceHandler.Preserve
		};

		return Ok(JsonSerializer.Serialize(basket_item, jsonOptions));
	}

	[HttpGet(template: "get-all")]
	public IActionResult Get()
	{
		try
		{
			return Ok(_basketService.FetchAllBasketItems());
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, " processing error.");

			return StatusCode(500, exception.Message);
		}
	}
	[HttpGet(template: "get/{ID}")]
	public IActionResult Get(Guid ID)
	{
		try
		{
			var DTO = _basketService.FetchSingleBasketItem(ID);

			return Ok(DTO);
		}
		catch (Exception exception)
		{
			throw new Exception(exception.Message, exception);
		}
	}
	[HttpDelete(template: "delete/{ID}")]
	public IActionResult Delete(Guid ID)
	{
		try
		{
			_basketService.ClearSingleBasketItemFromBasketData(ID);
			return NoContent();
		}
		catch (Exception exception)
		{
			throw new Exception(exception.Message, exception);
		}
	}
	[HttpDelete(template: "delete-all")]
	public IActionResult Delete()
	{
		_basketService.ClearBasketItems();

		return NoContent();
	}
}
