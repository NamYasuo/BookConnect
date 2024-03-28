using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Ecom.Rating
{
	public class RatingRecord
	{
		[Key]
		public Guid RatingRecordId { get; set; }
		public Guid RatingId { get; set; }
		public Guid UserId { get; set; }
		public int RatingPoint { get; set; }
		public string? Comment { get; set; }

		[ForeignKey("RatingId"), JsonIgnore]
		public virtual Rating? Rating { get; set; }
        [ForeignKey("UserId"), JsonIgnore]
        public virtual AppUser? AppUser { get; set; }
    }
}

