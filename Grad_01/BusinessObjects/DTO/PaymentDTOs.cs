using System;
namespace BusinessObjects.DTO
{
    public class NewTransactionDTO
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = "VND";
        public int? RequiredAmount { get; set; }
        public string? ReferenceId { get; set; }
        //public string? PaymentLanguage { get; set; } = "vn";

        //public string? PaymentDestinationId { get; set; } = string.Empty;
        //public string? Signature { get; set; } = string.Empty;
    }

    public class PaymentLinkDTO
    {
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
    }

    public class PaymentReturnDTO
    {
        public string? PaymentId { get; set; }
        /// <summary>
        /// 00: Success
        /// 99: Unknown
        /// 10: Error
        /// </summary>
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
        /// <summary>
        /// Format: yyyyMMddHHmmss
        /// </summary>
        public string? PaymentDate { get; set; }
        public string? PaymentRefId { get; set; }
        public decimal? Amount { get; set; }
        public string? Signature { get; set; }
    }

    public class VnPayResponseDTO
    {
        public decimal? vnp_Amount { get; set; }

        public string vnp_BankCode { get; set; } = string.Empty;

        public string vnp_BankTranNo { get; set; } = string.Empty;

        public string vnp_CardType { get; set; } = string.Empty;

        public string vnp_OrderInfo { get; set; } = string.Empty;

        public string vnp_PayDate { get; set; } = string.Empty;

        public string? vnp_ResponseCode { get; set; }

        public string vnp_TmnCode { get; set; } = string.Empty;

        public string vnp_TransactionNo { get; set; } = string.Empty;

        public string vnp_TransactionStatus { get; set; } = string.Empty;

        public string vnp_TxnRef { get; set; } = string.Empty;

        public string vnp_SecureHash { get; set; } = string.Empty;

        //public string vnp_SecureHashType { get; set; } = string.Empty;

        //vnp_Amount, vnp_BankCode, vnp_BankTranNo, vnp_CardType, vnp_OrderInfo, vnp_PayDate, vnp_ResponseCode, vnp_TmnCode, vnp_TransactionNo, vnp_TransactionStatus, vnp_TxnRef, vnp_SecureHash
    }

    public class VnPayRequestDTO
    {
        //public Guid PaymentId { get; set; }

        //1. vnp_Version Alphanumeric[1,8] required
        public string? vnp_Version { get; set; }

        //2. vnp_Command Alpha[1,16] required
        public string? vnp_Command { get; set; }

        //3. vnp_TmnCode Alphanumeric[8] required
        public string? vnp_TmnCode { get; set; }

        //4. vnp_Amount Numeric[1,12] required
        public decimal? vnp_Amount { get; set; }

        //5. vnp_BankCode Alphameric[3, 20] optional
        public string? vnp_BankCode { get; set; }

        //6. vnp_CreateDate Numeric[14] required
        public string? vnp_CreateDate { get; set; }

        //7. vnp_CurrCode Alpha[3] required
        public string? vnp_CurrCode { get; set; }

        //8. vnp_IpAddr Alphanumeric[7,45] required
        public string? vnp_IpAddr { get; set; }

        //9. vnp_Locale Alpha[2,5] required
        public string? vnp_Locale { get; set; }

        //10. vnp_OrderInfo Alphanumeric[1,255] required
        public string? vnp_OrderInfo { get; set; }

        //11. vnp_OrderType Alpha[1, 100] optional
        public string? vnp_OrderType { get; set; }

        //12. vnp_ReturnUrl Alphanumeric[10, 255] required
        public string? vnp_ReturnUrl { get; set; }

        //13. vnp_TxnRef Alphanumeric[1, 100] required
        public string? vnp_TxnRef { get; set; }

        //14. vnp_SecureHash Alphanumeric[32, 256] required
        public string? vnp_SecureHash { get; set; }

        //15 vnp_ExpireDate extra attribute
        public string? vnp_ExpireDate { get; set; }
    }
}

