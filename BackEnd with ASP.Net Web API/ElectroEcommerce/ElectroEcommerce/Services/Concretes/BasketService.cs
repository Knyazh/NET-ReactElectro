using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase;
using ElectroEcommerce.DataBase.DTOs.Basket;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Migrations;
using ElectroEcommerce.Services.Abstracts;
using System.Text.Json;

namespace ElectroEcommerce.Services.Concretes;

public class BasketService : IBasketService
{
	 private readonly IHttpContextAccessor _http_context_accessor;
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public BasketService(IHttpContextAccessor httpContextAccessor, IUserService user_service, DataContext data_context, IFileService file_service)
        {
            _http_context_accessor = httpContextAccessor;
		    _userService = user_service;
		    _dataContext = data_context;
		    _fileService = file_service;
        }
        public DataBase.Models.BasketItem AppendProductToBasket(BasketCookie basketCookieItem)
        {
            List<BasketCookie> basketCookieItemList = new List<BasketCookie>();
		DataBase.Models.BasketItem basketItem = new DataBase.Models.BasketItem();

            var cookieBasketValue = _http_context_accessor.HttpContext.Request.Cookies[CookieNames.BasketItems.ToString()];
            var basketItemsData = _dataContext.BasketItems.Where(b => b.CurrentUserID
            .Equals(_userService.CurrentUser.Id)).ToList();

            if (cookieBasketValue is null && basketItemsData.Count == 0)
            {
                basketCookieItemList.Add(basketCookieItem);

                basketItem = ConstructBasketItem(basketCookieItem);
            }

            if (cookieBasketValue is null && basketItemsData.Count > 0)
            {
                basketCookieItemList.Add(basketCookieItem);

                basketCookieItemList = SyncBasketCookieItemsWithCookieData(basketCookieItemList, basketItemsData);

                basketItem = ConstructBasketItem(basketCookieItem);
            }

            if (cookieBasketValue is not null && basketItemsData.Count > 0)
            {
                basketCookieItemList = JsonSerializer.Deserialize<List<BasketCookie>>(cookieBasketValue);

                basketCookieItemList = basketCookieItemList.Count > 0 ? basketCookieItemList : new List<BasketCookie>();        

                if (basketCookieItemList.Count > 0 && basketCookieItemList
                    .Any(basket_cookie_item => basket_cookie_item.CurrentUserId != _userService.CurrentUser.Id))
                {
                    ClearBasketItems();

                    basketCookieItemList.Add(basketCookieItem);
				    basketCookieItemList = SyncBasketCookieItemsWithCookieData(basketCookieItemList, basketItemsData);
				    basketItem = ConstructBasketItem(basketCookieItem);
                }

			if (basketCookieItemList.Count > 0 && (basketCookieItemList
			  .Any(basketCookieItem => basketCookieItem.CurrentUserId != _userService.CurrentUser.Id) is false))
			{
				if (basketCookieItemList.Count != basketItemsData.Count)
				{
					basketCookieItemList = SyncBasketCookieItemsWithCookieData(basketCookieItemList, basketItemsData);
				}
				if (basketCookieItemList.Any(basket_cookie_item => basket_cookie_item.ProductID.Equals(basket_cookie_item.ProductID))
					&& _dataContext.BasketItems.Any(bi => bi.ProductID.Equals(basketCookieItem.ProductID)))
				{
					var exist_basket_item_data = _dataContext.BasketItems.SingleOrDefault(bi => bi.ProductID.Equals(basketCookieItem.ProductID));
					var exist_basket_item_cookie = basketCookieItemList.SingleOrDefault(bi => bi.ProductID.Equals(basketCookieItem.ProductID));

					if (exist_basket_item_cookie is not null && exist_basket_item_data is not null)
					{
						basketCookieItemList.Remove(exist_basket_item_cookie);
						_dataContext.BasketItems.Remove(_dataContext.BasketItems.Single(bi => bi.ProductID.Equals(exist_basket_item_data.ProductID)));
						_dataContext.SaveChanges();
						BasketCookie updatedBasketCookieItem;
                        DataBase.Models.BasketItem updatedBasketItem;

						UpdateBasketItem(exist_basket_item_cookie, exist_basket_item_data, basketCookieItem, out updatedBasketCookieItem, out updatedBasketItem);

						basketCookieItemList.Add(updatedBasketCookieItem);
						basketItem = updatedBasketItem;
					}
				}
				else
				{
					basketCookieItemList.Add(basketCookieItem);

					basketItem = ConstructBasketItem(basketCookieItem);
				}
			}
		}

		cookieBasketValue = JsonSerializer.Serialize(basketCookieItemList);
		_http_context_accessor.HttpContext.Response.Cookies.Append(CookieNames.BasketItems.ToString(), cookieBasketValue);

		return basketItem;
	}
        public List<BasketListItemDTO> FetchAllBasketItems()
        {
            var basketItemsCookieValue = _http_context_accessor.HttpContext.Request.Cookies[CookieNames.BasketItems.ToString()];

            var basketCookieItemsList = basketItemsCookieValue != null
                ? JsonSerializer.Deserialize<List<BasketCookie>>(basketItemsCookieValue)
                : new List<BasketCookie>();
            
            if(basketCookieItemsList.Count > 0 && basketCookieItemsList.Any(b => b.CurrentUserId.Equals(_userService.CurrentUser.Id)))
            {
                var DTOs = basketCookieItemsList.Select(b => new BasketListItemDTO
                {
                    ProductName = b.ProductName, 
                    PhisicalimageURLs = _fileService
                    .ReadStaticFiles(b.ProductPrefix, Contracts.CustomUploadDirectories.Products, b.PhisicalImageNames.ToList()).ToArray(),
                    Price = b.Price,
                    IsAviable = b.IsAviable,
                    Quantity = b.Quantity,
                    ProductId = b.ProductID,
                    ColorID = b.ColorID,

                }).ToList();

                return DTOs;
            }
            else
            {
                var DTOs = _dataContext.BasketItems.Where(b => b.CurrentUserID.Equals(_userService.CurrentUser.Id))
                    .Select(b => new BasketListItemDTO
                    {
                        ProductName = b.ProductName,
                        PhisicalimageURLs = _fileService
                        .ReadStaticFiles(b.ProductPrefix, Contracts.CustomUploadDirectories.Products, b.PhisicalImageNames.ToList()).ToArray(),
                        Price= b.Price,
                        IsAviable= b.IsAviable,
                        Quantity= b.Quantity,  
                        ProductId= b.ProductID,
						ColorID = b.ColorID
					}).ToList();

                return DTOs;    
            }
        }


	public BasketListItemDTO FetchSingleBasketItem(Guid ID)
	{
		var DTOs = FetchAllBasketItems();

		var DTO = DTOs.SingleOrDefault(DTO => DTO.ProductId.Equals(ID));

		return DTO is null
			? throw new Exception("The shopping cart item you were looking for was not found," +
			" Please try again later ")
			: DTO;
	}
	public void ClearSingleBasketItemFromBasketData(Guid ID)
	{
		var basket_items_cookie_value = _http_context_accessor.HttpContext.Request.Cookies[CookieNames.BasketItems.ToString()];

		var basket_cookie_items_list = basket_items_cookie_value != null
			? JsonSerializer.Deserialize<List<BasketCookie>>(basket_items_cookie_value)
			: new List<BasketCookie>();

		if (basket_cookie_items_list.Count > 0 && basket_cookie_items_list.Any(bi => bi.CurrentUserId.Equals(_userService.CurrentUser.Id)))
		{
			var basket_cookie_item = basket_cookie_items_list.SingleOrDefault(bi => bi.ProductID.Equals(ID));

			if (basket_cookie_item is null)
				throw new Exception("The shopping cart item  was not found!," +
			" Please try again later ");

			basket_cookie_items_list.Remove(basket_cookie_item);

			var basket_item = _dataContext.BasketItems.SingleOrDefault(bi => bi.ProductID.Equals(ID));

			if (basket_item is null)
				throw new Exception("The shopping cart item  was not found!," +
			" Please try again later");

			_dataContext.BasketItems.Remove(basket_item);
			_dataContext.SaveChanges();

			basket_items_cookie_value = JsonSerializer.Serialize(basket_cookie_items_list);
			_http_context_accessor.HttpContext.Response.Cookies.Append(CookieNames.BasketItems.ToString(), basket_items_cookie_value);
		}
		else
		{
			var basket_item = _dataContext.BasketItems.SingleOrDefault(bi => bi.ProductID.Equals(ID));

			if (basket_item is null)
				throw new Exception("The shopping cart item was not found!," +
			" Please try again later");

			_dataContext.BasketItems.Remove(basket_item);
			_dataContext.SaveChanges();
		}
	}
	private void UpdateBasketItem(BasketCookie basket_cookie_exist_item, DataBase.Models.BasketItem basket_cookie_exist_data,
            BasketCookie basket_cookie_item, out BasketCookie updatedBasketCookieItem, out DataBase.Models.BasketItem updatedBasketItem)
        {

            basket_cookie_exist_item.Price = basket_cookie_item.Price;
            basket_cookie_exist_item.Quantity = basket_cookie_item.Quantity;
            basket_cookie_exist_item.IsAviable = basket_cookie_item.IsAviable;
            basket_cookie_exist_item.ColorID = basket_cookie_item.ColorID;

            basket_cookie_exist_data.ColorID = basket_cookie_item.ColorID;
            basket_cookie_exist_data.UpdatedAt = DateTime.UtcNow;
            basket_cookie_exist_data.Quantity = basket_cookie_item.Quantity;
            basket_cookie_exist_data.IsAviable = basket_cookie_item.IsAviable;
            basket_cookie_exist_data.Price = basket_cookie_item.Price;

            updatedBasketCookieItem = basket_cookie_exist_item;
            updatedBasketItem = basket_cookie_exist_data;
        }
      
        private List<BasketCookie> SyncBasketCookieItemsWithCookieData(List<BasketCookie> basket_cookie_items, List<DataBase.Models.BasketItem> basket_items_data)
        {
            foreach (var basket_item_data in basket_items_data)
            {
                BasketCookie basketCookie = new BasketCookie
                {
                    CurrentUserId = basket_item_data.CurrentUserID,
                    ProductID = basket_item_data.ProductID,
                    ProductName = basket_item_data.ProductName,
                    PhisicalImageNames = basket_item_data.PhisicalImageNames,
                    ProductPrefix = basket_item_data.ProductPrefix,
                    Price = basket_item_data.Price,
                    Quantity = basket_item_data.Quantity,
                    IsAviable = basket_item_data.IsAviable,
                    ColorID = basket_item_data.ColorID,
                };

                basket_cookie_items.Add(basketCookie);
            }

            return basket_cookie_items;
        }

        private DataBase.Models.BasketItem ConstructBasketItem(BasketCookie basket_cookie_item)
        {
            var basket_item = new DataBase.Models.BasketItem
            {
                ProductID = basket_cookie_item.ProductID,
                ColorID = basket_cookie_item.ColorID,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Quantity = basket_cookie_item.Quantity,
                IsAviable = basket_cookie_item.IsAviable,
                ProductName = basket_cookie_item.ProductName,
                ProductPrefix = basket_cookie_item.ProductPrefix,
                PhisicalImageNames = basket_cookie_item.PhisicalImageNames,
                Price = basket_cookie_item.Price,
                CurrentUserID = basket_cookie_item.CurrentUserId
            };

            return basket_item;
        }


	

	public void ClearBasketItems()
	{
		_http_context_accessor.HttpContext.Response.Cookies.Delete(CookieNames.BasketItems.ToString());
	}
}

