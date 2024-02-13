using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.User;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Errors;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ElectroEcommerce.CustomEx;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ElectroEcommerce.Controllers;

	[ApiController]
	[Route("api/v1/users")]
public class AuthenticationController : ControllerBase
{
	
		private readonly DataContext _dataContext;
		private readonly IVerificationService _verificationService;
		private readonly IFileService _fileService;
		private readonly IActivationService _activationSerive;
		private readonly INotificationService _notificationService;
		private readonly IUserService _user_Service;

		public AuthenticationController(DataContext dataContext,
			IVerificationService verificationService,
			IFileService fileService,
			IActivationService activationSerive,
			INotificationService notificationService,
			IUserService user_Service)
		{
			_dataContext = dataContext;
			_verificationService = verificationService;
			_fileService = fileService;
			_activationSerive = activationSerive;
			_notificationService = notificationService;
			_user_Service = user_Service;
		}

		[HttpPost("auth/register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);

			}

			if (await _dataContext.Users.AnyAsync(u => u.Email.Equals(userRegisterDto.Email)))
			{
				ModelState.Clear();
				ModelState.AddModelError(CustomErrors.Key.Email.ToString(),
					CustomErrors.Value.EMAIL_EXIST_ERROR);
				return BadRequest(ModelState);
			}

			if (await _dataContext.Users.AnyAsync((u => u.PhoneNumber.Equals(userRegisterDto.PhoneNumber))))
				{
				ModelState.Clear();
				ModelState.AddModelError(CustomErrors.Key.PhoneNumber.ToString(),
					CustomErrors.Value.PHONENUMBER_EXIST_ERROR);
				return BadRequest(ModelState);
			}

			User user = new()
			{
				Name = userRegisterDto.Name,
				LastName = userRegisterDto.LastName,
				Email = userRegisterDto.Email,
				Password = _verificationService.HashPassword(userRegisterDto.Password),
				PhoneNumber = userRegisterDto.PhoneNumber,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow,
				UserPrefix = _verificationService.RandomFolderPrefixGenerator(Prefix.USER)
			};

			if(userRegisterDto.File is not null)
			{
				user.PhysicalImageUrl= await _fileService.UploadAsync(CustomUploadDirectories.Users, userRegisterDto.File,user.UserPrefix);
			}

			var transaction = await _dataContext.Database.BeginTransactionAsync();
			using (transaction)
			{
				try
				{

					await _dataContext.Users.AddAsync(user);
					await _dataContext.SaveChangesAsync();

					var activationToken = await _activationSerive
						.GenerateAndSendURL(user, Guid.NewGuid().ToString());

					Dictionary<string, string> OldAndNewValues = new()
					{
						{ "{email}", user.Email },
						{ "{surname}", user.LastName },
						{ "{name}", user.Name }
					};

					await _notificationService.PrepareAndSendSMSNotifcation(OldAndNewValues, user.PhoneNumber, SmsTemplate.Value.activationURL);
					await _dataContext.ActivationTokens.AddAsync(activationToken);
					await _dataContext.SaveChangesAsync();

					await transaction.CommitAsync();

				var jsonOptions = new JsonSerializerOptions
				{
					ReferenceHandler = ReferenceHandler.Preserve
				};

				var URL = "https://localhost:7010/api/v1/users/details" + user.Id;
				return Created(URL, JsonSerializer.Serialize(user, jsonOptions));

			}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception(ex.Message,ex);
				}
			}


		}

	[HttpGet("auth/verify")]
	public async Task<IActionResult> Verify([FromQuery(Name = "Id")] Guid Id, [FromQuery(Name = "Token")] string Token)
	{
		try
		{
			var token = await _dataContext.ActivationTokens.SingleOrDefaultAsync(token => token.UserId.Equals(Id) && token.UniqueActivationToken.Equals(Token));

			var user = await _dataContext.Users.SingleOrDefaultAsync(user => user.Id.Equals(Id));


			if (user is null || token is null)
			{
				throw new ActivationException();
			}

			if (token.IsUsed is true && user.IsComfirmed is true)
			{
				await _notificationService
					.PrepareAndSendEmailNotifcation(user, EmailTemplate.Subject.Activation_Email,
					EmailTemplate.Body.Exist_Account_Email);
				throw new ActivationException();
			}

			if (token.ExpireDate < DateTime.UtcNow)
			{
				await _notificationService
					.PrepareAndSendEmailNotifcation(user,
					EmailTemplate.Subject.Expired_Token,
					EmailTemplate.Body.Expired_Token);
				_dataContext.ActivationTokens.Remove(token);
				await _dataContext.SaveChangesAsync();
				throw new ActivationException();
			}


			user.IsComfirmed = true;
			user.ApplicationPassword = _verificationService.GenerateAppPassword();
			token.IsUsed = true;
			token.LastUpdatedAt = DateTime.UtcNow;
			user.UpdatedAt = DateTime.UtcNow;
			user.ConfirmedDate = DateTime.UtcNow;
			

			Dictionary<string, string> data = new Dictionary<string, string>();
			data.Add("{Name}", user.Name);
			data.Add("{Surname}", user.LastName);
			data.Add("{app_password}", user.ApplicationPassword);

			var email = await _notificationService
				.PrepareAndSendEmailNotifcation(data,
				user, EmailTemplate.Subject.Success_Activation,
				EmailTemplate.Body.Activation_Email);

			await _notificationService
				.PrepareAndSendSMSNotifcation(data, user.PhoneNumber,
				EmailTemplate.Body.Activation_Email);

			_dataContext.Emails.Add(email);
			_dataContext.Update(user);
			_dataContext.Update(token);
			await _dataContext.SaveChangesAsync();

		}
		catch (Exception ex)
		{
			throw new ActivationException("Activation user field", ex);
		}

		return Ok();
	}


	[HttpPost("auth/login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Login([FromForm] UserLogInDto DTO)
	{

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		else if (!string.IsNullOrEmpty(DTO.Email))
		{
			var result = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email.Equals(DTO.Email));
			ModelState.Clear();
			ModelState.AddModelError("Email", "Email not found!");
			if (result is null) return BadRequest(ModelState);
		}

		var password = _verificationService.HashPassword(DTO.Password);

		var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Password.Equals(password));
		if (user is null)
		{
			ModelState.Clear();
			ModelState.AddModelError("Password", "The password you entered is incorrect, please try again!");
			return BadRequest(ModelState);
		}

		if (!user.IsComfirmed)
		{
			ModelState.Clear();
			ModelState.AddModelError("Login-Error", "Sorry dear user, your account has not been approved!");
			return BadRequest(ModelState);
		}

		var claims = new List<Claim>
			{
				 new Claim("Id", user.Id.ToString()),
			};

		claims.AddRange(_user_Service.GetClaimsAccordingToRole(user));

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

		return Ok(user);
	}

	[HttpGet("auth/logout")]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		return NoContent();
	}
}
