using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Ecom
{
	public class Cart
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CartId { get; set; }
		public Guid CustomerId { get; set; }
		public int? Total_Quantity { get; set; }
		public double? Total_Price { get; set; }
		public DateTime? ExpiredDate { get; set; }
		public string? Status { get; set; } = null!;
		public DateTime? CreateDate { get; set; }
		public DateTime? LastUpdatedDate { get; set; }

		[ForeignKey("CustomerId"), JsonIgnore]
		public virtual AppUser AppUser { get; set; } = null!;
	}
}

