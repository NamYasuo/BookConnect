//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;

//namespace BusinessObjects.Models.Ecom.Payment
//{
//	public class PaymentDetails
//	{
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        public Guid PaymentId { get; set; }
//		public string Content { get; set; } = null!;
//		public string? Currency { get; set; }
//		public decimal? RequiredAmount { get; set; }
//		public DateTime? PaidDate { get; set; }
//		//public DateTime ExpireDate { get; set; } ?
//		public string? Language { get; set; } 
//		public Guid MerchantId { get; set; }
//		//public Guid Payment_destination_id { get; set; }
//		public string? PaymentGate { get; set; } 
//		public string? Status { get; set; } 
//		public string? LastMessage { get; set; }

//		[ForeignKey("MerchantId"), JsonIgnore]
//		public virtual AppUser? Merchant { get; set; } 

//	}



