using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.Order;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace ElectroEcommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{

	private readonly IBasketService _basketService;
	private readonly IUserService _userService;
	private readonly IVerificationService _verificationService;
	private readonly DataContext _dataContext;
	private readonly IEmailService _emailService;
	private readonly IOrderService _orderService;

	public OrderController(IBasketService basketService, 
		IUserService userService, IVerificationService
		verificationService, DataContext dataContext, 
		IEmailService emailService, IOrderService orderService)
	{
		_basketService = basketService;
		_userService = userService;
		_verificationService = verificationService;
		_dataContext = dataContext;
		_emailService = emailService;
		_orderService = orderService;
	}

	[HttpPost(template: "create-order")]
	public async Task<IActionResult> Post()
	{
		List<OrderItem> Order_Items = new List<OrderItem>();
		OrderDetailsDTO orderDetailsDTO = new OrderDetailsDTO();
		var basket_items_data = _basketService.FetchAllBasketItems();
		decimal total = 0;
		Order order = new()
		{
			UserId = _userService.CurrentUser.Id,
			CurrentOrderStatus = OrderStatus.Created.ToString(),
			TrackingCode = _verificationService.RandomFolderPrefixGenerator(Prefix.ORDER),
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow,
		};

		if (basket_items_data.Count == 0)
			throw new Exception("Not implemet");

		foreach (var basket_item_data in basket_items_data)
		{
			var color = await _dataContext.Colors.SingleOrDefaultAsync(c => c.Id.Equals(basket_item_data.ColorID));
			var product = await _dataContext.Products.SingleOrDefaultAsync(pr => pr.Id.Equals(basket_item_data.ProductId));

			if (color != null && product != null)
			{
				OrderItem Order_Item = new()
				{
					Order = order,
					ProductID = basket_item_data.ProductId,
					ProductColorID = color.Id,
					Quantity = basket_item_data.Quantity,
					OrderItemSinglePrice = product.Price,
					OrderItemTotalPrice = product.Price * basket_item_data.Quantity,
					PhisicalImageName = basket_item_data.PhisicalimageURLs.Length > 0 ? basket_item_data.PhisicalimageURLs[0] : string.Empty,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
					ProductPrefix = product.ProductPrefix
				};

				OrderItemsDetailsDTO orderItemsDetailsDTO = new OrderItemsDetailsDTO
				{
					ProductCode = product.ProductPrefix,
					ProductName = product.Name,
					BrandName = _dataContext.Brands.Single(br => br.Id.Equals(product.CurrentBrandId)).Name,
					CategoryName = _dataContext.Categories.Single(ctg => ctg.Id.Equals(product.CurrentCategoryId)).Name,
					OrderItemSinglePrice = Order_Item.OrderItemSinglePrice,
					OrderItemTotalPrice = Order_Item.OrderItemTotalPrice,
					Quantity = Order_Item.Quantity,
					PhisicalImageURL = Order_Item.PhisicalImageName,
					CreatedAt = Order_Item.CreatedAt
				};

				orderDetailsDTO.Order_Item_Details_DTOs.Add(orderItemsDetailsDTO);

				total += product.Price  * basket_item_data.Quantity;

				Order_Items.Add(Order_Item);
			}
		}

		order.OrderItems = Order_Items;
		order.OrderTotalPrice = total;


		await _dataContext.Orders.AddAsync(order);

		_basketService.ClearBasketItems();
		_dataContext.BasketItems
			.RemoveRange(_dataContext.BasketItems
			.Where(bi => bi.CurrentUserID.Equals(_userService.CurrentUser.Id)));

		await _dataContext.SaveChangesAsync();

		orderDetailsDTO.OrderID = order.Id;
		orderDetailsDTO.OrderCreatedAt = order.CreatedAt;
		orderDetailsDTO.OrderTrackingCode = order.TrackingCode;
		orderDetailsDTO.CurrentOrderStatus = order.CurrentOrderStatus;
		orderDetailsDTO.CurrentUserName = _userService.CurrentUser.Name;
		orderDetailsDTO.CurrentUserSurname = _userService.CurrentUser.LastName;
		orderDetailsDTO.CurrentUserEmail = _userService.CurrentUser.Email;
		orderDetailsDTO.CurrentUserPhoneNumber = _userService.CurrentUser.PhoneNumber;
		orderDetailsDTO.SummaryTotal = order.OrderTotalPrice;

		var htmlcontent = await _orderService.PrepareAndSendOrderInvoiceAsync(orderDetailsDTO);

		var document = new PdfDocument();

		PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.LargePost);

		byte[]? response = null;
		using (MemoryStream ms = new MemoryStream())
		{
			document.Save(ms);
			response = ms.ToArray();
		}
		string Filename = "Invoice_" + order.TrackingCode + ".pdf";
		return File(response, "application/pdf", Filename);

	}
}
