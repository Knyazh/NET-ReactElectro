namespace ElectroEcommerce.DataBase.DTOs.User;

public class UserListItemDto
{
	public string Name { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string PhisicalImageURL { get; set; } = string.Empty;
	public bool IsAdmin { get; set; } = false;

	public string ApplicationPassword { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
