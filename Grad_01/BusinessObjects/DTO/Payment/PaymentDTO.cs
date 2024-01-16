using System;
namespace APIs.DTO.Payment
{
	public class PaymentDTO
	{
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = "VND";
        public decimal? RequiredAmount { get; set; }
        //public string? PaymentLanguage { get; set; } = "vn";
        //public string? MerchantId { get; set; } = string.Empty;
        //public string? PaymentDestinationId { get; set; } = string.Empty;
        //public string? Signature { get; set; } = string.Empty;
    }
}

