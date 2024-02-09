using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.DTOs.User;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Errors;
using ElectroEcommerce.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers;

public class AuthenticationController
{
	[ApiController]
	[Route("api/v1/users")]
	public class UserController : ControllerBase
	{
		private readonly DataContext _dataContext;
		private readonly IVerificationService _verificationService;
		private readonly IFileService _fileService;
		private readonly IActivationService _activationSerive;
		private readonly INotificationService _notificationService;
		private readonly IUserService _user_Service;

		public UserController(DataContext dataContext,
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
		public async Task<IActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
		{
			if (ModelState.IsValid)
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

			if(userRegisterDto.File != null)
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

					Dictionary<string, string> OldAndNewValues = new Dictionary<string, string>
					{
						{ "{email}", user.Email },
						{ "{surname}", user.LastName },
						{ "{name}", user.Name }
					};

					await _notificationService.PrepareAndSendSMSNotifcation(OldAndNewValues, user.PhoneNumber, SmsTemplate.Value.activationURL);
					await _dataContext.ActivationTokens.AddAsync(activationToken);
					await _dataContext.SaveChangesAsync();

					await transaction.CommitAsync();


					return Ok();

				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception(ex.Message,ex);
				}
			}


		}
	}
}
