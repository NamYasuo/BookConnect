using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Payment
	{
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public Guid PaymentId { get; set; }
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchantId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;
        public string? Signature { get; set; } = string.Empty;

        //1. vnp_Version Alphanumeric[1,8] required
        //2. vnp_Command Alpha[1,16] required
        //3. vnp_TmnCode Alphanumeric[8] required
        //4. vnp_Amount Numeric[1,12] required
        //5. vnp_BankCode Alphameric[3, 20] optional
        //6. vnp_CreateDate Numeric[14] required 
        //7. vnp_CurrCode Alpha[3] required
        //8. vnp_IpAddr Alphanumeric[7,45] required
        //9. vnp_Locale Alpha[2,5] required 
        //10. vnp_OrderInfo Alphanumeric[1,255] required
        //11. vnp_OrderType Alpha[1, 100] optional
        //12. vnp_ReturnUrl Alphanumeric[10, 255] required
        //13. vnp_TxnRef Alphanumeric[1, 100] required
        //14. vnp_SecureHash Alphanumeric[32, 256] required
    }
}

