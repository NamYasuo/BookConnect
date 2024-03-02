using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
	public class Work
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid WorkId { get; set; }
		public string Titile { get; set; } = null!;
		public Guid AuthorId { get; set; }
        public string Type { get; set; } = null!; //Values: Public or Private
        public string Status { get; set; } = null!; //Values: Published or not
        public string? CoverDir { get; set; }
        public string? BackgroundDir { get; set; }
        public string? Description { get; set; } 

        [ForeignKey("AuthorId"), JsonIgnore]
        public virtual AppUser Author { get; set; } = null!;
	}
}

