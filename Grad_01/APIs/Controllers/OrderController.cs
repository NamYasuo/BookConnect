using System;
using APIs.Repositories.Interfaces;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models.Ecom.Payment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ITransactionService _transactionService;
        public OrderController(IOrderService orderService, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create-order")]
        public IActionResult CreateOrder([FromBody] CheckoutDTO request)
        {

            decimal totalAmount = _orderService.GetTotalAmount(request.Products);
  
            Guid orderId = Guid.NewGuid();
            NewOrderDTO newOrder = new NewOrderDTO();

            switch (request.PaymentMethod)
            {
                case "VnPay": {
                        newOrder.OrderId = orderId;
                        newOrder.CustomerId = request.CustomerId;
                        newOrder.Price = request.PaymentReturnDTO.Amount;
                        newOrder.Status = request.PaymentReturnDTO?.PaymentStatus;
                        newOrder.Notes = request.PaymentReturnDTO.PaymentMessage;
                        newOrder.TransactionId = Guid.Parse(request.PaymentReturnDTO.PaymentRefId);
                        newOrder.PaymentMethod = request.PaymentMethod;
                        newOrder.AddressId = request.AddressId;
                    }
                    break;
                case "COD":
                    {
                        newOrder.OrderId = orderId;
                        newOrder.CustomerId = request.CustomerId;
                        newOrder.Price = totalAmount;
                        newOrder.Status = "It's a COD, what do u think ?";
                        newOrder.Notes = "Told u, it's a COD ?";
                        newOrder.PaymentMethod = request.PaymentMethod;
                        newOrder.AddressId = request.AddressId;
                        newOrder.TransactionId = null;
                    }
                break;
            }

            string result = _orderService.CreateNewOrder(newOrder);

            if (result == "Successfully!")
            {
                string result2 = _orderService.TakeProductFromCartOptional(request.CustomerId, orderId, request.Products);

                return Ok(result2);

                //if (result2 == "Successfully!")
                //{
                //    return Ok("Successfully!HIHI");
                //}
                //return BadRequest(result2);
            }
            return BadRequest(result);
        }

       
        [HttpPost]
        [Route("check-out")]
        public async Task<IActionResult> CheckoutAsync([FromBody] PreCheckoutDTO dto)
        {

            foreach(ProductOptionDTO p in dto.Products)
            {
                if(_orderService.GetCurrentStock(p.ProductId) < p.Quantity)
                {
                    return BadRequest("Can't purchase more product than inside stock!");
                }
            }

            decimal totalAmount = _orderService.GetTotalAmount(dto.Products);
            int truncatedAmount = (int)Math.Round(totalAmount, MidpointRounding.AwayFromZero);



            NewTransactionDTO newTransDTO = new NewTransactionDTO()
            {
                PaymentContent = "Bill" + Guid.NewGuid(),
                PaymentCurrency = "vnd",
                RequiredAmount = truncatedAmount,
                ReferenceId = dto.ReferenceId.ToString()
            };

            using (HttpClient client = new HttpClient())
            {
                // Convert the request data to JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(newTransDTO);

                // Prepare the HTTP request content
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Make the POST request and include the request content in the body
                HttpResponseMessage response = await client.PostAsync("https://localhost:7138/api/Payment/vnpay/create-vnpay-link", content);
;
                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    // Request successful
                    string? responseJson = await response.Content.ReadAsStringAsync();

                    PaymentLinkDTO? link = JsonConvert.DeserializeObject<PaymentLinkDTO>(responseJson);

                    return Ok(link?.PaymentUrl);
                }
                else
                {
                    // Request failed
                    return BadRequest("API request failed with status code: " + response.StatusCode);
                }
            }


            //Guid orderId = Guid.NewGuid();
            //NewOrderDTO dto = new NewOrderDTO()
            //{
            //    OrderId = orderId,
            //    CustomerId = request.CustomerId,
            //    Status = data?.PaymentStatus,
            //    Notes = data?.PaymentMessage,
            //    PaymentId = data != null ? Guid.Parse(data.PaymentId) : Guid.Empty,
            //    AddressId = request.AddressId,
            //};
            //string result = _orderService.CreateNewOrder(dto);

            //if (result == "Successfully!")
            //{
            //    string result2 = _orderService.TakeProductFromCartOptional(request.CustomerId, orderId, request.Products);

            //    if (result2 == "Successfully!")
            //    {
            //        return Ok("Successfully!");
            //    }
            //    return BadRequest(result2);
            //}
            //return BadRequest(result);
        }

        [HttpGet("get-transaction-by-id")]
        public IActionResult GetTransactionById(Guid refId)
        {
            try
            {
                TransactionRecord? record = _transactionService.GetTransactionId(refId);
                if (record != null) return Ok(record); 
                else return BadRequest("Not found!");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}