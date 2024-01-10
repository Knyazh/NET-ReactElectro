using ElectroEcommerce.DataBase;
using ElectroEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers;

[ApiController]
[Route("api-v1-user")]
public class UserController : ControllerBase
{
	private readonly DataContext _dataContext;

	public UserController(DataContext dataContext)
	{
		_dataContext = dataContext;
	}



	[HttpPost("add-user")]
	public async Task<ActionResult<List<User>>> AddUser(User user)
	{
		_dataContext.Users.Add(user);
		await _dataContext.SaveChangesAsync();
		return Ok(await _dataContext.Users.ToListAsync());
	}



}
