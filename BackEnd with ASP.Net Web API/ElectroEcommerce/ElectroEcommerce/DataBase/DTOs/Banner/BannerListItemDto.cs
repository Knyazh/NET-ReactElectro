﻿using ElectroEcommerce.DataBase.Base;

namespace ElectroEcommerce.DataBase.DTOs.Banner;

public class BannerListItemDto : BaseEntity<Guid>, IAuditable
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string BannerPrefix { get; set; }

	public string File { get; set; }
	public DateTime CreatedAt { get ; set ; }
	public DateTime UpdatedAt { get; set; }
}
