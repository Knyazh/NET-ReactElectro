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
using ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Concretes;

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
		private readonly ILogger<AuthenticationController> _logger;

	public AuthenticationController(DataContext dataContext,
		IVerificationService verificationService,
		IFileService fileService,
		IActivationService activationSerive,
		INotificationService notificationService,
		IUserService user_Service,
		ILogger<AuthenticationController> logger)
	{
		_dataContext = dataContext;
		_verificationService = verificationService;
		_fileService = fileService;
		_activationSerive = activationSerive;
		_notificationService = notificationService;
		_user_Service = user_Service;
		_logger = logger;
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

	[HttpGet("get-all")]
	[Produces(type: typeof(List<UserListItemDto>))]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]

	public async Task<IActionResult> Get()
	{
		try
		{
			var users = await _dataContext.Users.Where(u => u.IsComfirmed == true && u.Role == Role.Values.User)
				  .Select(u => new UserListItemDto
				  {
					  Name = u.Name,
					  LastName = u.LastName,
					  PhoneNumber = u.PhoneNumber,
					  Email = u.Email,
					  CreatedAt = u.CreatedAt,
					  UpdatedAt = u.UpdatedAt,
					  IsAdmin = u.IsAdmin,
					  PhisicalImageURL = _fileService
					  .ReadStaticFiles(u.UserPrefix, CustomUploadDirectories.Users, u.PhysicalImageUrl)
				  }).OrderBy(u => u.Name).OrderBy(u => u.CreatedAt).ToListAsync();

			return Ok(users);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error proccessing");
			return StatusCode(500,"Proccessing error try again");
		}
	}


	[HttpGet("get/{Id}")]
	
	public async Task<IActionResult> Get( [FromRoute]Guid Id)
	{
		try
		{
			var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id.Equals(Id));
			if (user is null)
				return NotFound($"The user with the << {Id} >> number you are looking for does not already exist in the database!");

			var response = new UserListItemDto
			{
				Name = user.Name,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Email = user.Email,
				CreatedAt = user.CreatedAt,
				UpdatedAt = user.UpdatedAt,
				IsAdmin = user.IsAdmin,
				PhisicalImageURL = _fileService
					.ReadStaticFiles(user.UserPrefix, CustomUploadDirectories.Users, user.PhysicalImageUrl)
			};

			return Ok(response);

		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "processing error.");

			return StatusCode(500, "Please try again later.");
		}
	}

	[HttpDelete("delete/{applicationPassword}")]

	public async Task<IActionResult> Delete([FromRoute] string applicationPassword)
	{
		try
		{
			var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.ApplicationPassword.Equals(applicationPassword));
			if (user == null) return NotFound("User Cant Found");

			if (user.PhysicalImageUrl is not null)
			{
				_fileService.RemoveStaticFiles(user.UserPrefix,
					CustomUploadDirectories.Users, user.PhysicalImageUrl);
			}

			_dataContext.Users.Remove(user);
			await _dataContext.SaveChangesAsync();
			return NoContent();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "error processing ");

			return StatusCode(500, "Please try again later.");
		}
	}




	[HttpGet("details/{Id}")]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	[Produces(type: typeof(UserListItemDto))]
	public async Task<IActionResult> Details([FromRoute] Guid Id)
	{
		try
		{
			var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id.Equals(Id));
			if (user is null)
				return NotFound($"User Cant Found");

			var response = new UserDetailsDto
			{
				Name = user.Name,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Password = user.Password,
				Email = user.Email,
				CreatedAt = user.CreatedAt,
				ConfirmedDate = user.ConfirmedDate,
				UpdatedAt = user.UpdatedAt,
				ApplicationPassword = user.ApplicationPassword,
				IsComfirmed = user.IsComfirmed,
				PhisicalImageURL = _fileService
					.ReadStaticFiles(user.UserPrefix, CustomUploadDirectories.Users, user.PhysicalImageUrl)
			};

			return Ok(response);
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "error  processing.");

			return StatusCode(500, "Please try again later.");
		}
	}


	[HttpPut("update/{applicationPassword}")]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	[Consumes("multipart/form-data")]
	public async Task<IActionResult> Update([FromRoute] string applicationPassword, [FromForm] UserUpdateDto userUpdateDto)
	{
		try
		{
			var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.ApplicationPassword.Equals(applicationPassword));
			if (user == null) return NotFound($"The user with this <<{applicationPassword}>> password was not found in the database!");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (userUpdateDto.File is not null)
			{
				_fileService.RemoveStaticFiles(user.UserPrefix, CustomUploadDirectories.Users, user.PhysicalImageUrl);
				await _fileService.UploadAsync(CustomUploadDirectories.Users, userUpdateDto.File, user.UserPrefix);

			}
			user.Name = userUpdateDto.Name;
			user.LastName = userUpdateDto.LastName;
			user.PhoneNumber = userUpdateDto.PhoneNumber;
			user.Password = _verificationService.HashPassword(userUpdateDto.Password);
			user.UpdatedAt = DateTime.UtcNow;

			_dataContext.Users.Update(user);
			await _dataContext.SaveChangesAsync();

			return Ok(user);
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "error processing .");

			return StatusCode(500, " Please try again later.");
		}
	}






}
