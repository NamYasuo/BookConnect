using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class AppUser
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None),]
        public Guid UserId { get; set; }
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Salt { get; set; } = null!;
		public bool IsValidated { get; set; } = false;
		public Guid RoleId { get; set; }

        [ForeignKey("RoleId"), JsonIgnore]
        public virtual Role? Role { get; set; }
	}
}

