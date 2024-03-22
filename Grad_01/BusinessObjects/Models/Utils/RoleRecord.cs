using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Utils
{
	public class RoleRecord
	{
		public Guid UserId { get; set; }
		public Guid RoleId { get; set; }

		[ForeignKey("UserId"), JsonIgnore]
		public AppUser User { get; set; } = null!;
        [ForeignKey("RoleId"), JsonIgnore]
        public Role Role { get; set; } = null!;
	}
}

