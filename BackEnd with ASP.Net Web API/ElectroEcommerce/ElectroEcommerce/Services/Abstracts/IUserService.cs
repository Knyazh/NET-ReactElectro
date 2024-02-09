using ElectroEcommerce.DataBase.Models;
using System.Security.Claims;

namespace ElectroEcommerce.Services.Abstracts;

public interface IUserService
{
	bool IsCurrentUserAuthenticated();
	List<Claim> GetClaimsAccordingToRole(User user);
}
