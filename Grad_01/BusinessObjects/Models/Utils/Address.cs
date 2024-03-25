using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Address
	{
        [Key]
		public Guid AddressId { get; set; }
		public string? City_Province { get; set; } 
		public string? District { get; set; } 
		public string? SubDistrict { get; set; }
		public string Rendezvous { get; set; } = null!;
		public bool Default { get; set; } = false;
		public Guid? UserId { get; set; }

		[ForeignKey("UserId"), JsonIgnore]
		public AppUser? User { get; set; } 

	}
}

