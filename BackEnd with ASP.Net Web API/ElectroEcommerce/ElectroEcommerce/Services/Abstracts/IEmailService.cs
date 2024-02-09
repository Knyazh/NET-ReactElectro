using ElectroEcommerce.DataBase.DTOs.Email;

namespace ElectroEcommerce.Services.Abstracts;

public interface IEmailService
{
	Task SendEmailAsync(string reciepent, string subject, string body);
	Task SendEmailAsync(List<string> reciepents, string subject, string body);
}