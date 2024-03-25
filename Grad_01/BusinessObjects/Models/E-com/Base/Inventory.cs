using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Inventory
	{
		[Key]
		public Guid InventoryId { get; set; }
		public int Quantity { get; set; }

        public Guid AgencyId { get; set; }
        [ForeignKey("AgencyId"), JsonIgnore]
		public virtual Agency Agency { get; set; } = null!;

        public Guid BookId { get; set; }
        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book Book { get; set; } = null!;

	}
}

