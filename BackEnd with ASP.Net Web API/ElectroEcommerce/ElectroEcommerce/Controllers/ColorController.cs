using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ElectroEcommerce.Controllers;


[ApiController]
[Route("api/v1/colors")]
public class ColorController : ControllerBase
{
	private readonly DataContext _dataContexxt;
	private readonly ILogger<ColorController> _logger;
	public ColorController(ILogger<ColorController> logger, DataContext dataContexxt)
	{
		_logger = logger;
		_dataContexxt = dataContexxt;
	}
	[HttpGet("get-all")]
	[Produces(type: typeof(List<Color>))]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get()
	{
		try
		{
			var colors = await _dataContexxt.Colors.ToListAsync();

			return Ok(colors);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");

			return StatusCode(500, ex.Message);
		}
	}
	[HttpGet("get/{Id}")]
	[Produces(type: typeof(Size))]
	[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get([FromRoute] Guid Id)
	{
		try
		{
			var color = await _dataContexxt.Colors.SingleOrDefaultAsync(c => c.Id.Equals(Id));

			if (color is null) return NotFound($"The color << {Id} >>does not exist yet!");

			return Ok(color);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Processing error");

			return StatusCode(500, ex.Message);
		}
	}
}

