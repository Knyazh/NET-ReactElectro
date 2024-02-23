using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.Order;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Services.Concretes;

public class OrderService : IOrderService
{
	private readonly IEmailService _emailService;
	private readonly IUserService _userService;
	public OrderService(IEmailService email_service, IUserService user_service)
	{
		_emailService = email_service;
		_userService = user_service;
	}

	public async Task<string> PrepareAndSendOrderInvoiceAsync(OrderDetailsDTO DTO)
	{
		var emailBody = InVoice.GenerateInvoiceHtml(DTO);
		await _emailService
			.SendEmailAsync(_userService.CurrentUser.Email, EmailTemplate.Subject.Order_Invoice, emailBody);

		return emailBody;
	}
}
