using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Ecom.Payment
{
	public class Payment
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PaymentId { get; set; }
		public string? Content { get; set; }
		public string Currency { get; set; } = null!;
		//public string? ref_id { get; set; }
		public decimal RequiredAmount { get; set; }
		public DateTime PaidDate { get; set; }
		public DateTime ExpireDate { get; set; }
		public string Language { get; set; } = null!;
		public Guid MerchantId { get; set; }
		//public Guid Payment_destination_id { get; set; }
		public decimal PaidAmount { get; set; }
		public string Status { get; set; } = null!;
		public string LastMessage { get; set; } = null!;

		[ForeignKey("MerchantId"), JsonIgnore]
		public virtual AppUser? Merchant { get; set; } 

	}
}

