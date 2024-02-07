using ElectroEcommerce.Services.Abstracts;
using System.Net.Mail;
using System.Net;

public class EmailSender : IEmailSender
{
	public async Task SendEmailAsync(string email, string subject, string message)
	{
		try
		{
			var mail = "knyazheydarov@gmail.com";
			var pw = "Knyaz3030";

			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(mail, pw)
			};

			await client.SendMailAsync(
				new MailMessage(
					from: mail,
					to: email,
					subject: subject,
					body: message
				));
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to send email: {ex.Message}");
			throw; 
		}
	}
}
