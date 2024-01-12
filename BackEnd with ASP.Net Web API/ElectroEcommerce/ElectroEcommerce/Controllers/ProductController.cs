using ElectroEcommerce.DataBase;
using ElectroEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Controllers;

[Route("api/v1/product")]
[ApiController]
public class ProductController : ControllerBase
{

	DataContext _dataContext;
	public ProductController(DataContext dataContext)
	{
		_dataContext = dataContext;
	}


	[HttpPost("add-product")]
	public async Task<ActionResult<List<ProductModel>>> Add(ProductModel product)
	{
		_dataContext.Products.Add(product);
		await _dataContext.SaveChangesAsync();
		return Ok(await _dataContext.Users.ToListAsync());
	}
}
