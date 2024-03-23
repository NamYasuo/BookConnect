using System;
using BusinessObjects.Models.Ecom.Payment;

namespace BusinessObjects.DTO
{
	public class TransactionMetadataDTO
	{
        public Guid? TransactionId { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? BankCode { get; set; }
        public string? CardType { get; set; }
        public string? OrderInfo { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
    }

    public class TransactionDetailsDTO
    {
        public Guid? TransactionId { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? BankCode { get; set; }
        public string? CardType { get; set; }
        public string? OrderInfo { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
        public string? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Signature { get; set; }
    }

}

