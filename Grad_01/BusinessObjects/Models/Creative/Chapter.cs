using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Utils;
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
		public int ChapterNumber { get; set; }
		public DateTime CreateDate { get; set; }
		public string Type { get; set; } = null!; //Values: Public or Private
		public string Status { get; set; } = null!; //Values: Published or not
		public Guid StatId { get; set; } 

		[ForeignKey("WorkId"), JsonIgnore]
		public virtual Work Work { get; set; } = null!;

		[ForeignKey("StatId"), JsonIgnore]
		public virtual Statistic Stats { get; set; } = null!;
	}
} 
