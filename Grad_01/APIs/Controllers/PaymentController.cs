using System.Net;
using APIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models.Ecom.Payment;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using APIs.Utils.Paging;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using APIs.Utils;

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
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
                BankCode = response.vnp_BankCode,
                CardType = response.vnp_CardType,
                OrderInfo = response.vnp_OrderInfo,
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
        //IQueryable<TransactionRecord> queryable = _transacService.GetAllTransaction();

        //[HttpGet("get-all-transaction")]
        //[EnableQuery]
        //public IActionResult GetAllTransaction([FromQuery] ODataQueryOptions<TransactionRecord> oData, PagingParams @params)
        //{
        //    try
        //    {
        //        var filteredQueryable = oData.ApplyTo(_transacService.GetAllTransaction()) as IQueryable<TransactionRecord>;

        //        var pagedRecords = PagedList<TransactionRecord>.ToPagedList(filteredQueryable, @params.PageNumber, @params.PageSize);

        //        return Ok(filteredQueryable);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}

