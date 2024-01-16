using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class ProductDetail
	{
		public Guid AgencyId { get; set; }
		public Guid BookId { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime PublishDate { get; set; }

		[ForeignKey("AgencyId"), JsonIgnore]
		public virtual Agency Agency { get; set; } = null!;
        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book Book { get; set; } = null!;

	}
}

