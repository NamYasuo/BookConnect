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
        public Guid UserId { get; set; }
		public string? City_Province { get; set; } = null!;
		public string? District { get; set; } = null!;
		public string? SubDistrict { get; set; } = null!;
		public string? Rendezvous { get; set; }
		public bool Default { get; set; } = false;

		[ForeignKey("UserId"), JsonIgnore]
		public virtual AppUser? AppUser { get; set; } = null!;
	}
}

