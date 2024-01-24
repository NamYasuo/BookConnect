using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Ecom.Payment;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Ecom
{
	public class Order
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrderId { get; set; }
		public Guid CustomerId { get; set; }
		public decimal? Total_Price { get; set; }
		public string? Status { get; set; }

		public int Quantity { get; set; }
		public string? Notes { get; set; }
		public DateTime CreatedDate { get; set; }
		//public Guid PaymentId { get; set; }
		public Guid? AddressId { get; set; }


		//[ForeignKey("PaymentId"), JsonIgnore]
  //      public virtual PaymentDetails? Payment { get; set; }
		[ForeignKey("AddressId"), JsonIgnore]
		public virtual Address? Address { get; set; }

	}
}

