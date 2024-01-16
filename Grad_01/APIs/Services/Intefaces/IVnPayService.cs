using System;
using APIs.DTO;
using APIs.DTO.Payment;
using APIs.Utils.Base.Models;
using BusinessObjects.Models;

namespace APIs.Services.Intefaces
{
	public interface IVnPayService
	{
        public BaseResultWithData<(PaymentReturnDTO, string)> ProcessVnPayReturn(VnPayResponseDTO request);
        public string NewTransaction(PaymentDTO paymentInfo);
        //public string GetLink(string baseUrl, string secretKey, Payment requestDto);
    }
}

