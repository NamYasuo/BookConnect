using System;
using System.ComponentModel.DataAnnotations;

namespace APIs.DTO
{
	public class RoleDTO
	{
		[Required]
		public string RoleName { get; set; } = null!;
		public string? Description { get; set; }

	}
}

