using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.Basket;
using ElectroEcommerce.Services.Abstracts;
using System.Text.Json;

namespace ElectroEcommerce.Services.Concretes;

public class BasketService : IBasketService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public BasketService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public void AddToBasket(Guid productId, Guid colorId, int? quantity)
	{
		var basketItemCookieViewModels = new List<BasketItemDto>();
		var basketItemCookieViewModel = new BasketItemDto();

		var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[CookieNames.BASKET_ITEMS];
		if (cookieValue != null)
		{
			basketItemCookieViewModels = JsonSerializer.Deserialize<List<BasketItemDto>>(cookieValue);
			basketItemCookieViewModel = GetBasketItem(basketItemCookieViewModels, productId, colorId);

			if (basketItemCookieViewModel != null)
			{
				UpdateBasketItemQuantity(basketItemCookieViewModel, quantity);
			}
			else
			{
				basketItemCookieViewModel = InitializerNewBasketItem(productId, colorId,  quantity);
				basketItemCookieViewModels.Add(basketItemCookieViewModel);
			}

		}
		else
		{
			basketItemCookieViewModel = InitializerNewBasketItem(productId, colorId,  quantity);
			basketItemCookieViewModels.Add(basketItemCookieViewModel);
		}

		cookieValue = JsonSerializer.Serialize(basketItemCookieViewModels);

		_httpContextAccessor.HttpContext.Response.Cookies.Append(CookieNames.BASKET_ITEMS, cookieValue);
	}


	private BasketItemDto InitializerNewBasketItem(Guid productId, Guid colorId, int? quantity)
	{
		return new BasketItemDto
		{
			ProductId = productId,
			ColorId = colorId,
			Quantity = quantity ?? 1
		};
	}

	private void UpdateBasketItemQuantity(BasketItemDto model, int? quantity)
	{
		model.Quantity += (quantity ?? 1);
	}

	private BasketItemDto GetBasketItem(List<BasketItemDto> basketCookieItemViewModels, Guid productId, Guid colorId)
	{
		return basketCookieItemViewModels.FirstOrDefault(m =>
			m.ProductId == productId &&
			m.ColorId == colorId);
	}

	public List<BasketItemDto> GetBasketItemsFromCookie()
	{
		var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[CookieNames.BASKET_ITEMS];

		return cookieValue != null ?
			JsonSerializer.Deserialize<List<BasketItemDto>>(cookieValue) :
			new List<BasketItemDto>();
	}

	public void ClearBasket()
	{
		_httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieNames.BASKET_ITEMS);
	}
}

