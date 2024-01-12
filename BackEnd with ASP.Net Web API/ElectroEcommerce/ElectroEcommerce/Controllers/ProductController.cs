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
	readonly DataContext _dataContext;
	public ProductController(DataContext dataContext)
	{
		_dataContext = dataContext;
	}


	[HttpPost("add-product")]
	public async Task<ActionResult<List<ProductModel>>> Add(ProductModel product)
	{
		product.Id = Guid.NewGuid();
		_dataContext.Products.Add(product);
		await _dataContext.SaveChangesAsync();
		return Ok(await _dataContext.Products.ToListAsync());
	}

	[HttpGet("get-all-products")]
	public async Task<ActionResult<List<ProductModel>>> Get()
	{
		return Ok(await _dataContext.Products.ToListAsync());
	}

	[HttpGet("get-by-id/{id}")]
	public async Task<ActionResult<ProductModel>> Get(Guid id)
	{
		var product = await _dataContext.Products.FindAsync(id);

		if (product == null) { return BadRequest("product no found"); }

		return Ok(product);
	}

	[HttpGet("get-procuct-by-name/{name}")]
	public async Task<ActionResult<IEnumerable<ProductModel>>> Get(string name)
	{
		var products = await _dataContext.Products.
			Where(p=> p.Name.ToLower()== name.ToLower()).
			ToListAsync();

		if(products == null || !products.Any()) { return BadRequest("Porduct name cant found"); }
		return Ok(products);
	}

	[HttpPut("update-product-id{id}")]
	public async Task<ActionResult<List<ProductModel>>> Update(ProductModel request,Guid id)
	{
		var product = await _dataContext.Products.FindAsync(id);
		if (product == null) { return NotFound("Product not found"); }
		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.Brand = request.Brand;

		await _dataContext.SaveChangesAsync();

		return Ok(product);

	}

	[HttpDelete("remove-product-id/{id}")]
	public async Task<ActionResult<ProductModel>> Delete(Guid id)
	{
		var product = await _dataContext.Products.FindAsync(id); 
		if (product == null) { return NotFound("Product not found"); }
		_dataContext.Products.Remove(product);
		await _dataContext.SaveChangesAsync();
		return Ok(product);
	}
}
