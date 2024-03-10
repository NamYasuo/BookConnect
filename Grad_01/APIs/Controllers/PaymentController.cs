using System;
using System.Net;
using APIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using APIs.DTO;
using APIs.Config;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Models.Ecom.Payment;
using APIs.Utils.Extensions;
using BusinessObjects.DTO;
using Azure.Core;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController: ControllerBase
	{
        private readonly IVnPayService _vnpService;
        private readonly ITransactionService _transacService;

        public PaymentController(IVnPayService vnpService, ITransactionService transacService)
        {
            _vnpService = vnpService;
            _transacService = transacService;
        }

        [HttpPost]
        [Route("vnpay/create-vnpay-link")]
        //[ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDtos>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] NewTransactionDTO request)
        {
            var response = new PaymentLinkDTO();                        
            response.PaymentId = DateTime.Now.Ticks.ToString();
            response.PaymentUrl = _vnpService.NewTransaction(request);
            return Ok(response);
        }


        [HttpGet]
        [Route("vnpay/VnPayIPN")]
        public IActionResult VnpayIpnReturnAsync([FromQuery] VnPayResponseDTO response)
        {
            //string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDTO();
            var processResult = _vnpService.ProcessVnPayReturn(response);

            PaymentReturnDTO dto = processResult.Data.Item1;

            TransactionRecord transaction = new TransactionRecord()
            {
                //PaymentId = dto.PaymentId,
                PaymentDate = dto.PaymentDate,
                PaymentMessage = dto.PaymentMessage,
                TransactionId = Guid.Parse(dto.PaymentRefId),
                PaymentStatus = dto.PaymentStatus,
                Amount = dto.Amount,
                Signature = dto.Signature
            };

            _transacService.AddTransactionRecord(transaction);
            if (processResult.Success)
            {
                returnModel = processResult.Data.Item1;
                //returnUrl = processResult.Data.Item2;

                string redirectUrl = "http://localhost:5000/checkout-result?refId=" + returnModel.PaymentRefId;  // Replace with your desired URL
                return Redirect(redirectUrl);

                //return Ok("http://localhost:5000/checkout-result?refId=" + returnModel.PaymentRefId);
            }
            //    returnModel = processResult.Data.Item1;
            //    returnUrl = processResult.Data.Item2;
            //    if (returnUrl.EndsWith("/"))
            //    returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return BadRequest(returnModel);
        }


    }
}

