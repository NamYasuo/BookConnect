using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
	public class Subscription
	{
		public Guid SubId { get; set; }
		public Guid WorkId { get; set; }
		public float Price { get; set; }

		[ForeignKey("WorkId"), JsonIgnore]
		public virtual Work Work { get; set; } = null!;
	}
}

