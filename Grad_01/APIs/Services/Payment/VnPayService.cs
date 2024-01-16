using System.Net;
using System.Text;
using APIs.DTO;
using System.Security.Cryptography;
using APIs.Services.Intefaces;
using APIs.Config;
using Microsoft.Extensions.Options;
using APIs.DTO.Payment;
using APIs.Utils.Base.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using Azure;
using Azure.Core;
using Azure.Messaging;
using Microsoft.Data.SqlClient;

namespace APIs.Services
{
	public class VnPayService: IVnPayService
	{
        private readonly VnPayConfig _vnPayConfig;
        public VnPayService(IOptions<VnPayConfig> vnPayConfig)
		{
            _vnPayConfig = vnPayConfig.Value;
        }

        public string NewTransaction(PaymentDTO paymentInfo)
        {
            VnPayRequestDTO requestDTO = new VnPayRequestDTO
            {
                vnp_Locale = "vn",
                vnp_IpAddr = "243.130.96.200", //Replace with get user ip method or pass an ip param
                vnp_Version = _vnPayConfig.Version,
                vnp_CurrCode = paymentInfo.PaymentCurrency,
                vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss"),
                vnp_TmnCode = _vnPayConfig.TmnCode,
                vnp_Amount = paymentInfo.RequiredAmount * 100,
                vnp_Command = "pay",
                vnp_OrderType = "other",
                vnp_OrderInfo = paymentInfo.PaymentContent,
                vnp_ReturnUrl = _vnPayConfig.ReturnUrl,
                vnp_TxnRef = Guid.NewGuid().ToString(),
                vnp_ExpireDate = DateTime.Now.AddMinutes(6).ToString("yyyyMMddHHmmss"),
            };

            string secureHash = GetLink(_vnPayConfig.PaymentUrl, _vnPayConfig.HashSecret, requestDTO);

            return secureHash;
        }

        private string GetLink(string baseUrl, string secretKey, VnPayRequestDTO requestDTO)
        {
            SortedList<string, string> requestData = MakeRequestData(requestDTO);

            StringBuilder data = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            string result = baseUrl + "?" + data.ToString();
            CreateSecureHash(Encoding.UTF8.GetBytes(secretKey), data.ToString().Remove(data.Length - 1, 1), out var secureHash);
            return result += "vnp_SecureHash=" + secureHash;
        }

        public BaseResultWithData<(PaymentReturnDTO, string)> ProcessVnPayReturn(VnPayResponseDTO request)
        {
            string returnUrl = string.Empty;
            var result = new BaseResultWithData<(PaymentReturnDTO, string)>();

            try
            {
                var resultData = new PaymentReturnDTO();
                var isValidSignature = IsValidSignature(_vnPayConfig.HashSecret, request);

                if (isValidSignature)
                {
                    if (request.vnp_ResponseCode == "00")
                    {
                        resultData.PaymentStatus = "00";
                        //resultData.PaymentId = payment.Id;
                        ///TODO: Make signature
                        resultData.Signature = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        resultData.PaymentStatus = "10";
                        resultData.PaymentMessage = "Payment process failed";
                    }

                    result.Success = true;
                    result.Message = "OK";
                    result.Data = (resultData, returnUrl);
                }
                else
                {
                    resultData.PaymentStatus = "99";
                    resultData.PaymentMessage = "Invalid signature in response";
                }


            }
            catch (Exception ex)
            {
                result.Set(false, "Error");
                result.Errors.Add(new BaseError()
                {
                    Code = "Exeption",
                    Message = ex.Message,
                });
            }

            return result;
        }

        private bool IsValidSignature(string secretKey, VnPayResponseDTO responseDTO)
        {
            SortedList<string, string> responseData = MakeResponseData(responseDTO);
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in responseData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            CreateSecureHash(Encoding.UTF8.GetBytes(secretKey),
                data.ToString().Remove(data.Length - 1, 1), out string checkSum);
            return checkSum.Equals(responseDTO.vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private SortedList<string, string> MakeRequestData(VnPayRequestDTO requestDto)
        {
            SortedList<string, string> requestData = new SortedList<string, string>();
            if (requestDto.vnp_Amount != null)
                requestData.Add("vnp_Amount", requestDto.vnp_Amount.ToString() ?? string.Empty);
            if (requestDto.vnp_Command != null)
                requestData.Add("vnp_Command", requestDto.vnp_Command);
            if (requestDto.vnp_CreateDate != null)
                requestData.Add("vnp_CreateDate", requestDto.vnp_CreateDate);
            if (requestDto.vnp_CurrCode != null)
                requestData.Add("vnp_CurrCode", requestDto.vnp_CurrCode);
            if (requestDto.vnp_BankCode != null)
                requestData.Add("vnp_BankCode", requestDto.vnp_BankCode);
            if (requestDto.vnp_IpAddr != null)
                requestData.Add("vnp_IpAddr", requestDto.vnp_IpAddr);
            if (requestDto.vnp_Locale != null)
                requestData.Add("vnp_Locale", requestDto.vnp_Locale);
            if (requestDto.vnp_OrderInfo != null)
                requestData.Add("vnp_OrderInfo", requestDto.vnp_OrderInfo);
            if (requestDto.vnp_OrderType != null)
                requestData.Add("vnp_OrderType", requestDto.vnp_OrderType);
            if (requestDto.vnp_ReturnUrl != null)
                requestData.Add("vnp_ReturnUrl", requestDto.vnp_ReturnUrl);
            if (requestDto.vnp_TmnCode != null)
                requestData.Add("vnp_TmnCode", requestDto.vnp_TmnCode);
            if (requestDto.vnp_ExpireDate != null)
                requestData.Add("vnp_ExpireDate", requestDto.vnp_ExpireDate);
            if (requestDto.vnp_TxnRef != null)
                requestData.Add("vnp_TxnRef", requestDto.vnp_TxnRef);
            if (requestDto.vnp_Version != null)
                requestData.Add("vnp_Version", requestDto.vnp_Version);
            return requestData;
        }

        private SortedList<string, string> MakeResponseData(VnPayResponseDTO responseDto)
        {
            SortedList<string, string> responseData = new SortedList<string, string>
            {
                { "vnp_Amount", responseDto.vnp_Amount.ToString() ?? string.Empty }
            };
            if (!string.IsNullOrEmpty(responseDto.vnp_TmnCode))
                responseData.Add("vnp_TmnCode", responseDto.vnp_TmnCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_BankCode))
                responseData.Add("vnp_BankCode", responseDto.vnp_BankCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_BankTranNo))
                responseData.Add("vnp_BankTranNo", responseDto.vnp_BankTranNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_CardType))
                responseData.Add("vnp_CardType", responseDto.vnp_CardType.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_OrderInfo))
                responseData.Add("vnp_OrderInfo", responseDto.vnp_OrderInfo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_TransactionNo))
                responseData.Add("vnp_TransactionNo", responseDto.vnp_TransactionNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_TransactionStatus))
                responseData.Add("vnp_TransactionStatus", responseDto.vnp_TransactionStatus.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_TxnRef))
                responseData.Add("vnp_TxnRef", responseDto.vnp_TxnRef.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_PayDate))
                responseData.Add("vnp_PayDate", responseDto.vnp_PayDate.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(responseDto.vnp_ResponseCode))
                responseData.Add("vnp_ResponseCode", responseDto.vnp_ResponseCode ?? string.Empty);
            return responseData;
        }

        private void CreateSecureHash(byte[] key, string data, out string secureHash)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = key;
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            secureHash = hash.ToString();
        }


    }
}

