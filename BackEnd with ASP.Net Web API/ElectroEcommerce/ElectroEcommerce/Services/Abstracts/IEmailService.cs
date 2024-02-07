using ElectroEcommerce.DataBase.DTOs.Email;

namespace ElectroEcommerce.Services.Abstracts;

public interface IEmailService
{
	void SendEmail(EmailDto request);
}