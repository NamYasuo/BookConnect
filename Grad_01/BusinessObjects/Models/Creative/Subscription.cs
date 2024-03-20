using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
	public class Subscription
	{
		[Key]
		public Guid SubId { get; set; }
		public Guid TierId { get; set; }
		public Guid SubscriberId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Status { get; set; } = null!;

		//[ForeignKey("TierId"), JsonIgnore]
		//public virtual Tier Tier { get; set; } = null!;
		[ForeignKey("SubscriberId"), JsonIgnore]
		public virtual AppUser Subcriber { get; set; } = null!;
	}
}

