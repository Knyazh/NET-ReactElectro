using ElectroEcommerce.DataBase.DTOs.Email;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Hosting.Server;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Cms;
using System.Web;

namespace ElectroEcommerce.Services.Concretes;

public class ActivationService : IActivationService
{
	private readonly string _host;	
	private readonly IConfiguration _configuration;
	private readonly EmailService _emailService;

	public ActivationService(string host, IConfiguration configuration, EmailService emailService)
	{
		_host = _configuration.GetValue<string>("AppSettings:Host");
		_configuration = configuration;
		_emailService = emailService;
	}


	public async Task<ActivationToken> GenerateAndSendURL(User user, string activationToken)
	{
		var activationURL = $"{_host}api/v1/users/auth/verify?Id={user.Id}&Token={HttpUtility.UrlEncode(activationToken)}";

		var emeildDTO = new EmailDto
		{
			Subject = "Activation URL",
			Recipients = new List<string>()
			{
				user.Email,
			},
			Body = $@"Hello dear {user.LastName} {user.Name},<br><br>
                     You can activate your account by clicking the button below:<br><br>
                     <div style=""text-align: center;"">
                     <a href=""{activationURL}"" style=""display: inline-block; padding: 15px 45px; border-radius: 16px; background-color: #4CAF50; color: white; text-align: center; text-decoration: none; font-size: 18px; margin: 4px 2px; cursor: pointer;"">Activate Account</a>
                     </div>"
		};

		await _emailService.SendEmailAsync(emeildDTO.Recipients, emeildDTO.Subject, emeildDTO.Body);

		ActivationToken activationToken1 = new()
		{
			CreatedAt = DateTime.UtcNow,
			LastUpdatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow,
			UniqueActivationToken = activationToken,
			UserId = user.Id
		};

		return activationToken1;
	}


}
