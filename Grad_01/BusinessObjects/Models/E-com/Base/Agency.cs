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

        [ForeignKey("OwnerId"), JsonIgnore]
        public virtual AppUser Owner { get; set; } = null!;
	}
}

