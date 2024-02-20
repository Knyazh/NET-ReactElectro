using ElectroEcommerce.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce.Services.Concretes;

public class OrderService : IOrderService
{
	private readonly DataContext _dbContext;
	private readonly Random _random;
	private const string TRACKINGCODE_PREFIX = "OR";

	public OrderService(DataContext dbContext)
	{
		_dbContext = dbContext;
		_random = new Random();
	}

	public string GenerateTrackingCode()
	{
		string trackingCode;

		do
		{
			int numberPart = _random.Next(100000, 1000000);
			trackingCode = TRACKINGCODE_PREFIX + numberPart;

		} while (_dbContext.Orders.Any(o => o.TrackingCode == trackingCode));


		return trackingCode;
	}
}
