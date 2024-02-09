using Microsoft.AspNetCore.Mvc;

namespace ElectroEcommerce.Controllers;

public class AuthenticationController
{
	[ApiController]
	[Route("api/v1/users")]
	public class UserController : ControllerBase
	{
		private readonly DataContext _dataContext;

	}
}
