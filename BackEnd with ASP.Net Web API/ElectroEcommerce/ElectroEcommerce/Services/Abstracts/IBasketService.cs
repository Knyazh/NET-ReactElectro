using ElectroEcommerce.DataBase.DTOs.Basket;

namespace ElectroEcommerce.Services.Abstracts;

public interface IBasketService
{
	public void AddToBasket(Guid productId, Guid colorId, int? quantity);
	List<BasketItemDto> GetBasketItemsFromCookie();
	void ClearBasket();
}
