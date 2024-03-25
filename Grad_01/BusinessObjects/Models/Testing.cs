using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
	public class Testing
	{
		[Key]
		public Guid TestId { get; set; }
		public string Name { get; set; } = null!;
	}
}

