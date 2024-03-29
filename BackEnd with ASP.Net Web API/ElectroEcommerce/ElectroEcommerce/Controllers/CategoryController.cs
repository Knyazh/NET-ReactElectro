﻿using ElectroEcommerce.DataBase.DTOs.Category;
using ElectroEcommerce.DataBase.DTOs.Email;
using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
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


	[HttpGet("get-limited")]
	public async Task<ActionResult<List<Category>>> GetLimited( int limit)
	{
		return Ok(await _dataContext.Categories.Take(limit).ToListAsync());
	}

	[HttpPost("add-category")]

	public async Task<ActionResult<Category>> AddCategory( [FromForm]CategoryPostDTO categoryDTO)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var category = new Category
		{
			Name = categoryDTO.Name,
			Description = categoryDTO.Description,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};

		await _dataContext.AddAsync(category);
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

	

	[HttpPut("update-category/{id}")]
	public async Task<ActionResult<Category>> Update(  Guid id,  Category request)
	{
		var category = await _dataContext.Categories.FindAsync(id);
		if (category == null)
		{
			return NotFound("Category not found");
		}

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
