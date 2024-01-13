﻿namespace ElectroEcommerce.DataBase.Base;

public interface IAuditable
{
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set;}
}
