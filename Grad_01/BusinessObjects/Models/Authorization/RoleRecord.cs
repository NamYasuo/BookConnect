using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Authorization
{
	public class RoleRecord
	{
        [Key]
		public Guid RoleRecordId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId"), JsonIgnore]
		public AppUser? User { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId"), JsonIgnore]
        public Role? Role { get; set; }
	}
}

