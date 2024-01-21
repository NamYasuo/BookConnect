using System;
using APIs.DTO;
using APIs.Utils.Base.Models;
using BusinessObjects.DTO;
using BusinessObjects.Models;

namespace APIs.Services.Intefaces
{
	public interface IVnPayService
	{
        public BaseResultWithData<(PaymentReturnDTO, string)> ProcessVnPayReturn(VnPayResponseDTO request);
        public string NewTransaction(NewTransactionDTO paymentInfo);
        //public string GetLink(string baseUrl, string secretKey, Payment requestDto);
    }
}

