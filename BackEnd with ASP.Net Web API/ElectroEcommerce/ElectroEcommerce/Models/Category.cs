﻿using System.ComponentModel.DataAnnotations;

namespace ElectroEcommerce.Models;

public class Category
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}