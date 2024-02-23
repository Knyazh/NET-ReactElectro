using ElectroEcommerce.DataBase.DTOs.Basket;
using ElectroEcommerce.DataBase.Models;

namespace ElectroEcommerce.Services.Abstracts;

public interface IBasketService
{
	BasketItem AppendProductToBasket(BasketCookie basketCookie);
	List<BasketListItemDTO> FetchAllBasketItems();
	void ClearBasketItems();
	BasketListItemDTO FetchSingleBasketItem(Guid ID);
	void ClearSingleBasketItemFromBasketData(Guid ID);
}
