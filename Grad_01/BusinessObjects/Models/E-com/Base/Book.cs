﻿using System;
namespace BusinessObjects.Models
{
	public class Book: Product
	{
		public string? Type { get; set; } = null!;
		public int? Quantity { get; set; } 

	}
}

