using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.Models;
using System.Security.Claims;

namespace ElectroEcommerce.Services.Concretes;

public class UserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly DataContext _dataContext;
	private User _current_user;
	public UserService(IHttpContextAccessor contextAccessor, DataContext data_context)
	{
		_httpContextAccessor = contextAccessor;
		_dataContext = data_context;
	}

	public bool IsCurrentUserAuthenticated()
	{
		return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
	}

	public User CurrentUser
	{
		get
		{

			if (_current_user != null)
			{
				return _current_user;
			}

			if (_httpContextAccessor.HttpContext.User == null)
			{
				throw new Exception("User is not authenticated");
			}

			var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
			if (userIdClaim is null)
			{
				throw new Exception("User is not authenticated");
			}

			var userId = userIdClaim.Value;
			var user = _dataContext.Users.SingleOrDefault(u => u.Id.ToString() == userId);
			if (user is null)
			{
				throw new Exception("User not found in system");
			}

			_current_user = user;

			return _current_user;
		}
	}

	public List<Claim> GetClaimsAccordingToRole(User user)
	{
		var claims = new List<Claim>();

		switch (user.Role)
		{
			case Role.Values.User:
				claims.Add(new Claim(ClaimTypes.Role, Role.Names.User));
				break;

			case Role.Values.Admin:
				claims.Add(new Claim(ClaimTypes.Role, Role.Names.Admin));
				break;

			default:
				break;
		}

		return claims;
	}
}
