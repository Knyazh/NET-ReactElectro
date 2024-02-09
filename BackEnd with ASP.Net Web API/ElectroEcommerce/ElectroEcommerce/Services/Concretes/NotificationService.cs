using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using System.Text;

namespace ElectroEcommerce.Services.Concretes;

public class NotificationService : INotificationService
{
	private readonly ISmsService _smsService;
	private readonly IEmailService _emailService;
	public NotificationService(ISmsService smsService, IEmailService emailService)
	{
		_smsService = smsService;
		_emailService = emailService;
	}

	public async Task PrepareAndSendSMSNotifcation(Dictionary<string, string> OldAndNewValues, string recipient, string template)
	{
		if (OldAndNewValues == null || string.IsNullOrEmpty(template))
		{
			throw new ArgumentNullException("OldAndNewValues and template cannot be null or empty.");
		}

		StringBuilder templateBuilder = new StringBuilder(template);

		foreach (var keyValue in OldAndNewValues)
		{
			templateBuilder.Replace(keyValue.Key, keyValue.Value);
		}

		await _smsService.SendSMSAsync(recipient, templateBuilder.ToString());
	}

	public async Task PrepareAndSendSMSNotifcation(string recipient, string template)
	{
		if (string.IsNullOrEmpty(template))
		{
			throw new ArgumentNullException("Template cannot be null or empty.");
		}

		await _smsService.SendSMSAsync(recipient, template);
	}

	public async Task<Email> PrepareAndSendEmailNotifcation(User user, string subject, string body)
	{
		if (string.IsNullOrEmpty(body))
		{
			throw new ArgumentNullException("Template cannot be null or empty.");
		}

		var email = new Email()
		{
			Subject = subject,
			Body = body,
			Recipients = { user.Email },
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow,
			UserId = user.Id
		};

		await _emailService.SendEmailAsync(user.Email, subject, body);

		return email;
	}

	public async Task<Email> PrepareAndSendEmailNotifcation(Dictionary<string, string> OldAndNewValues, User user, string subject, string body)
	{
		if (OldAndNewValues == null || string.IsNullOrEmpty(body))
		{
			throw new ArgumentNullException("OldAndNewValues and template cannot be null or empty.");
		}

		var templateBuilder = new StringBuilder(body);

		foreach (var keyValue in OldAndNewValues)
		{
			templateBuilder.Replace(keyValue.Key, keyValue.Value);
		}

		var email = new Email()
		{
			Subject = subject,
			Body = templateBuilder.ToString(),
			Recipients = { user.Email },
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow,
			UserId = user.Id
		};

		await _emailService.SendEmailAsync(user.Email, email.Subject, email.Body);

		return email;
	}

}
