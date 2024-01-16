using System;
using APIs.DTO.Payment;
using System.Net;
using APIs.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using APIs.DTO;
using APIs.Config;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Models;
using APIs.Utils.Extensions;



namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController: ControllerBase
	{
        private readonly IVnPayService _vnpService;
        public PaymentController(IVnPayService vnpService)
        {
            _vnpService = vnpService;
        }

        [HttpPost]
        [Route("vnpay/create-vnpay-link")]
        //[ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDtos>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] PaymentDTO request)
        {
            var response = new PaymentLinkDTO();                        
            response.PaymentId = DateTime.Now.Ticks.ToString();
            response.PaymentUrl = _vnpService.NewTransaction(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("vnpay/vnpay-return")]
        public IActionResult VnpayReturn([FromQuery] VnPayResponseDTO response)
        {
            string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDTO();
            var processResult =  _vnpService.ProcessVnPayReturn(response);

            if (processResult.Success)
            {
                //returnModel = processResult.Data.Item1;
                returnUrl = processResult.Data.Item2;
            }

            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"{returnUrl}?{returnModel.ToQueryString()}");
        }

        //public async Task<IActionResult> VnPayIpnReturn([FromQuery] VnPayResponseDTO response)
        //{

        //}
    }
}

