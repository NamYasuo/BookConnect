using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Ecom.Rating;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Product
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProductId { get; set; }
		public string? Name { get; set; } = null!;
		public string? Description { get; set; }
		public double? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PublishDate { get; set; }

        public Guid? RatingId { get; set; }

		[ForeignKey("RatingId"), JsonIgnore]
		public virtual Rating? Rating { get; set; }
    }
}

