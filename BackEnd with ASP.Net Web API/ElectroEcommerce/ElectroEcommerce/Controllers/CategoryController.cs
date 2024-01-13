using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers;

[ApiController]
[Route("api/v1/category")]
public class CategoryController : ControllerBase
{
	private readonly DataContext _dataContext;
	public CategoryController(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	[HttpGet("get-all")]
	public async Task<ActionResult<List<Category>>> Get()
	{
		return Ok(await _dataContext.Categories.ToListAsync());
	}

	[HttpPost("add-category")]
	public async Task<ActionResult<List<Category>>> Add(Category category)
	{
		category.Id = Guid.NewGuid();
		await _dataContext.Categories.AddAsync(category);
		await _dataContext.SaveChangesAsync();
		return Ok(category);
	}

	[HttpGet("get-cagetory-id/{id}")]
	public async Task<ActionResult<Category>> Get(Guid id)
	{
		var category = await _dataContext.Categories.FindAsync(id);
		if (category == null) { return NotFound("Category not found"); }

			return Ok(category);
	}

	[HttpPut("update-categroy/{id}")]
	public async Task<ActionResult<List<Category>>> Update(Category request,Guid id)
	{
		var category = await _dataContext.Categories.FindAsync(id);
		if (category == null) { return NotFound("Category not exist"); }
		category.Name = request.Name;
		category.Description = request.Description;
		await _dataContext.SaveChangesAsync();
		return Ok(category);
	}

	[HttpDelete("delete-category-id/{id}")]
	public async Task<ActionResult<List<Category>>> Delete(Guid id)
	{
		var category = await _dataContext.Categories.FindAsync(id);
		if (category == null) { return NotFound("Category not found"); }
		_dataContext.Remove(category);
		await _dataContext.SaveChangesAsync();
		return Ok(category);
	}


}
