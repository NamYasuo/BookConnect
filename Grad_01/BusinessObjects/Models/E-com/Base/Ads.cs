using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
    public class Ads
    {
        [Key]
        public Guid AdsId { get; set; }

        public Guid AgencyId { get; set; }

        public required string Donors { get; set; }
        public string? Description { get; set; }
        public DateTime DateAdded { get; set; }

        [ForeignKey("AgencyId"), JsonIgnore]
        public virtual Agency Agency { get; set; } = null!;

    }
}
