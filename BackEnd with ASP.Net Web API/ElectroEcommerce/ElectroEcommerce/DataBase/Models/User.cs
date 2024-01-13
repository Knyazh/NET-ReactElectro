﻿using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.Models;

public class User : BaseEntity<Guid> , IAuditable
{

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
	public DateTime CreatedAt { get ; set ; }
	public DateTime UpdatedAt { get; set; }
}
