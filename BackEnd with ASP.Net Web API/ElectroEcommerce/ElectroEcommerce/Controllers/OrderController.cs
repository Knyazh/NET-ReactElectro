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
			[FromServices] DataContext dbContext,
			[FromServices] IUserService userService,
			[FromServices] IOrderService orderService,
			[FromServices] IFileService fileService,
			[FromServices] IBasketService basketService,
			[FromServices] IVerificationService verificationService)
			
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
				var product = dbContext.Products.Single(p => p.Id == basketItem.ProductId);
				var color = dbContext.Colors.Single(p => p.Id == basketItem.ColorId);

				var orderItem = new OrderItem
				{
					Order = order,
					ProductName = product.Name,
					ProductDescription = product.Description,
					ProductPrice = product.Price,
					ProductColorName = color.Name,
					OrderItemPrefix = verificationService.RandomFolderPrefixGenerator(Prefix.OrderItem),
					ProductQuantity = basketItem.Quantity
				};

				var productOrderPhotos = new List<string>();
				foreach (var imageName in product.PyshicalImageNames)
				{
					var productOrderPhoto = fileService.ReadStaticFiles(orderItem.OrderItemPrefix, CustomUploadDirectories.OrderItems, imageName);
					productOrderPhotos.Add(productOrderPhoto);
				}

				orderItem.ProductOrderPhotos = productOrderPhotos;
				total += basketItem.Quantity * product.Price;

				orderItems.Add(orderItem);
			}

			order.OrderItems = orderItems;


			dbContext.Orders.Add(order);
			dbContext.SaveChanges();

			basketService.ClearBasket();

			return Ok(new { Message = "Order placed successfully" });
		}
	}
}
