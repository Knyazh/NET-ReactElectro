using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class MyOrder : BaseEntity<Guid>, IAuditable
{
	public OrderStatus Status { get; set; }
	public string TrackingCode { get; set; }


	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
