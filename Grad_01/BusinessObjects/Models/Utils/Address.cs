using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Address
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid AddressId { get; set; }
		public string? City_Province { get; set; } = null!;
		public string? District { get; set; } = null!;
		public string? SubDistrict { get; set; } = null!;
		public string? Rendezvous { get; set; }
		public bool Default { get; set; } = false;
		public Guid? UserId { get; set; }

		[ForeignKey("UserId"), JsonIgnore]
		public AppUser? User { get; set; } 

	}
}

