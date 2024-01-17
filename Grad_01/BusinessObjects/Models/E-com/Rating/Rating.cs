using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models.Ecom.Rating
{
	public class Rating
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RatingId { get; set; }
		public Guid OverallRating { get; set; }
	}
}

