using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Utils
{
	public class CICMedia
	{
		public Guid UserId { get; set; }
		public string Directory { get; set; } = null!;
		public string Type { get; set; } = null!;

		[ForeignKey("UserId"), JsonIgnore]
		public virtual AppUser AppUser { get; set; } = null!;
	}
}

