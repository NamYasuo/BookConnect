using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Utils;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
	public class Work
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid WorkId { get; set; }
		public string Title { get; set; } = null!;
        public string Type { get; set; } = null!; //Values: Public or Private
        public string Status { get; set; } = null!; //Values: Published or not
        public decimal? Price { get; set; } 
        public string? CoverDir { get; set; }
        public string? BackgroundDir { get; set; }
        public string? Description { get; set; }

        public ICollection<Chapter>? Chapters { get; set; }

        public Guid StatId { get; set; }
        [ForeignKey("StatId"), JsonIgnore]
        public virtual Statistic Stats { get; set; } = null!;

        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId"), JsonIgnore]
        public virtual AppUser Author { get; set; } = null!;
	}
}

