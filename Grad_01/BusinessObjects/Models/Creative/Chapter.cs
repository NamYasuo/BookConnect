using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
	public class Chapter
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ChapterId { get; set;}
		public Guid WorkId { get; set; }
		public string Name { get; set; } = null!;
		public string? Directory { get; set; }
		public string Type { get; set; } = null!; //Values: Public or Private
		public string Status { get; set; } = null!; //Values: Published or not

		[ForeignKey("WorkId"), JsonIgnore]
		public virtual Work Work { get; set; } = null!;
	}
} 
