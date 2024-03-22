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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
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
        public async Task<IActionResult> VnpayIpnReturnAsync([FromQuery] VnPayResponseDTO response)
        {
            //string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDTO();
            var processResult = _vnpService.ProcessVnPayReturn(response);

            PaymentReturnDTO dto = processResult.Data.Item1;

            TransactionRecord transaction = new TransactionRecord()
            {
                //PaymentId = dto.PaymentId,
                //UserId = userId,
                PaymentDate = dto.PaymentDate,
                PaymentMessage = dto.PaymentMessage,
                TransactionId = Guid.Parse(dto.PaymentRefId),
                PaymentStatus = dto.PaymentStatus,
                Amount = dto.Amount,
                Signature = dto.Signature
            };
            int changes = _transacService.AddTransactionRecord(transaction);

            if (processResult.Success)
            {
                returnModel = processResult.Data.Item1;

                string redirectUrl = "http://localhost:5000/checkout-result?refId=" + returnModel.PaymentRefId;
                return Redirect(redirectUrl);
            }

            return BadRequest(returnModel);
        }

        [HttpPost("save-transactor"), Authorize]
        public IActionResult SaveTransactor(Guid transactionId)
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                Guid userId = (userIdClaim != null) ? Guid.Parse(userIdClaim.Value) : Guid.Empty;

                int changes = _transacService.IdentifyTransactor(transactionId, userId);
                IActionResult result = (changes > 0) ? Ok() : BadRequest("Fail to identify transactor!");
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

