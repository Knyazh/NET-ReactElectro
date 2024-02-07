namespace ElectroEcommerce.Services.Abstracts;

public interface IEmailSender
{
	Task SendEmailAsync(string email , string subject , string message);
}
