using ElectroEcommerce.Contracts;
using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class User : BaseEntity<Guid> , IAuditable
{

    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber {  get; set; } = string.Empty;
    public string UserPrefix { get; set; }
    public string PhysicalImageUrl {  get; set; } = string.Empty;
    public bool IsComfirmed { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

	public DateTime ConfirmedDate { get; set; }
	public string ApplicationPassword { get; set; } = string.Empty;
	public DateTime CreatedAt { get ; set ; }
	public DateTime UpdatedAt { get; set; }
    public Role.Values Role { get; set; }
    public List<Order> Orders { get; set; }
	public ActivationToken ActivationToken { get; set; }

}

