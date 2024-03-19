using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models.Ecom.Payment
{
	public class TransactionRecord
	{
        [Key]
        public Guid? TransactionId { get; set; }
        //public string? PaymentId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
        public string? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Signature { get; set; }
    }
}

