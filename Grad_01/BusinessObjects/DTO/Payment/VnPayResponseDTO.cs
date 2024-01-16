using System;
namespace APIs.DTO.Payment
{
	public class VnPayResponseDTO
	{
        public decimal vnp_Amount { get; set; }

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
}

