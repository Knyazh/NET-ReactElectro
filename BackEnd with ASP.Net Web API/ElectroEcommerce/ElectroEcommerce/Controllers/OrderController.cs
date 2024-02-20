using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{


		[HttpPost("place-order")]
		public ActionResult PlaceOrder(
			[FromServices] DbContext dbContext,
			[FromServices] IUserService userService,
			[FromServices] IOrderService orderService,
			[FromServices] IFileService fileService,
			[FromServices] IBasketService basketService)
		{
			var order = new Order
			{
				Status = OrderStatus.Created,
				TrackingCode = orderService.GenerateTrackingCode(),
				UserId = userService.CurrentUser.Id,
			};

			var basketItems = basketService.GetBasketItemsFromCookie();
			decimal total = 0;
			var orderItems = new List<OrderItem>();

			foreach (var basketItem in basketItems)
			{
				var product = pustokDbContext.Products.Single(p => p.Id == basketItem.ProductId);
				var color = pustokDbContext.Colors.Single(p => p.Id == basketItem.ColorId);
				var size = pustokDbContext.Sizes.Single(p => p.Id == basketItem.SizeId);

				var orderItem = new OrderItem
				{
					Order = order,
					ProductName = product.Name,
					ProductDescription = product.Description,
					ProductPrice = product.Price,
					ProductOrderPhoto = fileService
						.GetStaticFilesUrl(CustomUploadDirectories.Products, product.PhysicalImageName),
					ProductColorName = color.Name,
					ProductQuantity = basketItem.Quantity,
					ProductSizeName = size.Name,
				};

				total += basketItem.Quantity * product.Price;

				orderItems.Add(orderItem);
			}

			order.OrderItems = orderItems;

			_nofitificationService.SendOrderNotification(order);

			pustokDbContext.Orders.Add(order);
			pustokDbContext.SaveChanges();

			basketService.ClearBasket();

			return Ok(new { Message = "Order placed successfully" });
		}
	}
}
