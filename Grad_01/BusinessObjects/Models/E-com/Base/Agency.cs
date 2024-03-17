using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Agency
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AgencyId { get; set; }
		public string AgencyName { get; set; } = null!;
		public Guid OwnerId { get; set; }
        public Guid PostAddressId { get; set; }
        public string? LogoUrl { get; set; }
        public string BusinessType { get; set; } = null!;

        [ForeignKey("OwnerId"), JsonIgnore]
        public virtual AppUser Owner { get; set; } = null!;
        [ForeignKey("PostAddressId"), JsonIgnore]
        public virtual Address PostAddress { get; set; } = null!;
	}
}

