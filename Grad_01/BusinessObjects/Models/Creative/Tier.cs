using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models.Creative
{
	public class Tier
	{
		[Key]
		public Guid TierId { get; set; }
		public Guid CreatorId { get; set; }
		public decimal Price { get; set; }
		public string TierType { get; set; } = null!;
		public int Duration { get; set; }
		public string Status { get; set; } = null!; //Active or not

		[ForeignKey("CreatorId")]
		public virtual AppUser AppUser { get; set; } = null!;
	}
}

