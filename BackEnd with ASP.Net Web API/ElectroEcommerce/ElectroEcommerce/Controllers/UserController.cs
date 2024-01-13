using ElectroEcommerce.DataBase;
using ElectroEcommerce.DataBase.Models;
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

	[HttpGet("get-all-user")]
	public async Task<ActionResult<List<User>>> Get()
	{
		return Ok(await _dataContext.Users.ToListAsync());
	}

	[HttpGet("get-user-id/{id}")]

	public async Task<ActionResult<User>> Get(Guid id)
	{
		var user = await _dataContext.Users.FindAsync(id);

		if (user == null) { return BadRequest("User not found"); }

		return Ok(user);
	}


	[HttpGet("get-user-name/{name}")]
	public async Task<ActionResult<ICollection<User>>> Get(string name)
	{
		var users = await _dataContext.Users
			.Where(u => u.Name.ToLower() == name.ToLower())
			.ToListAsync();

		if (users == null || !users.Any())
		{
			return NotFound("No users found with the specified name.");
		}

		return Ok(users);
	}


	[HttpGet("get-user-email/{email}")]
	public async Task<ActionResult<User>> GetUserByEmail(string email)
	{
		var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
		if (user == null) { return NotFound("this email not found"); }
		return Ok(user);
	}


	[HttpPost("add-user")]
	public async Task<ActionResult<List<User>>> AddUser(User user)
	{
		user.Id = Guid.NewGuid();

		_dataContext.Users.Add(user);

		await _dataContext.SaveChangesAsync();

		return Ok(await _dataContext.Users.ToListAsync());
	}


	[HttpDelete("remove-user/{id}")]
	public async Task<ActionResult<User>> Delete(Guid id)
	{
		var user = await _dataContext.Users.FindAsync(id);

		if (user == null) { return NotFound("USer not found"); }

		_dataContext.Users.Remove(user);

		await _dataContext.SaveChangesAsync();

		return Ok(user);
	}

	[HttpPut("update-user/{id}")]
	public async Task<ActionResult<List<User>>> Update(User request)
	{
		var user = await _dataContext.Users.FindAsync(request.Id);
		if (user == null) { return NotFound("user not found"); }
		user.Name = request.Name;
		user.Email = request.Email;
		user.LastName = request.LastName;
		user.Password = request.Password;

		await _dataContext.SaveChangesAsync();
		return Ok(user);
	}





}
