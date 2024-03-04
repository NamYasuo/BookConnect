﻿using System;
using APIs.Repositories.Interfaces;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create-order")]
        public IActionResult CreateOrder([FromBody] CheckoutDTO request)
        {
            Guid orderId = Guid.NewGuid();
            NewOrderDTO dto = new NewOrderDTO()
            {
                OrderId = orderId,
                CustomerId = request.CustomerId,
                Status = request.PaymentReturnDTO?.PaymentStatus,
                Notes = request.PaymentReturnDTO.PaymentMessage,
                PaymentId = Guid.Parse(request.PaymentReturnDTO.PaymentId),
                AddressId = request.AddressId,
            };
            string result = _orderService.CreateNewOrder(dto);

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

        //		[HttpPost]
        //        [Route("create-order")]
        //        public async Task<IActionResult> CheckoutAsync([FromBody] CheckoutDTO request)
        //        {
        //            decimal totalAmount = _orderService.GetTotalAmount(request.Products);

        //            NewTransactionDTO newTransDTO = new NewTransactionDTO()
        //            {
        //                PaymentContent = "pay me please",
        //                PaymentCurrency = "vnd",
        //                RequiredAmount = totalAmount
        //            };

        //            PaymentReturnDTO? data = null;

        //            using (HttpClient client = new HttpClient())
        //            {

        //                // Convert the request data to JSON
        //                var json = Newtonsoft.Json.JsonConvert.SerializeObject(newTransDTO);

        //                // Prepare the HTTP request content
        //                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        //                // Make the POST request and include the request content in the body
        //                HttpResponseMessage response = await client.PostAsync("https://localhost:7138/api/Payment/vnpay/create-vnpay-link", content);

        //                // Check the response status code
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    // Request successful
        //                    string responseJson = await response.Content.ReadAsStringAsync();

        //                    data = JsonConvert.DeserializeObject<PaymentReturnDTO>(responseJson);
        //                }
        //                else
        //                {
        //                    // Request failed
        //                    Console.WriteLine("API request failed with status code: " + response.StatusCode);
        //                }
        //            }

        //            Guid orderId = Guid.NewGuid();
        //            NewOrderDTO dto = new NewOrderDTO()
        //            {
        //                OrderId = orderId,
        //                CustomerId = request.CustomerId,
        //                Status = data.PaymentStatus,
        //                Notes = data.PaymentMessage,
        //                PaymentId = Guid.Parse(data.PaymentId),
        //                AddressId = request.AddressId,
        //            };
        //            string result = _orderService.CreateNewOrder(dto);

        //            if (result == "Successfully!")
        //            {
        //                string result2 = _orderService.TakeProductFromCartOptional(request.CustomerId, orderId, request.Products);

        //                if (result2 == "Successfully!")
        //                {
        //                    return Ok("Successfully!");
        //                }
        //                return BadRequest(result2);
        //            }
        //            return BadRequest(result);
        //        }
        //    }
        //}
        [HttpPost]
        [Route("check-out")]
        public async Task<IActionResult> CheckoutAsync([FromBody] List<ProductOptionDTO> products)
        {
            decimal totalAmount = _orderService.GetTotalAmount(products);
            int truncatedAmount = (int)Math.Round(totalAmount, MidpointRounding.AwayFromZero);



            NewTransactionDTO newTransDTO = new NewTransactionDTO()
            {
                PaymentContent = "Bill no" + Guid.NewGuid(),
                PaymentCurrency = "vnd",
                RequiredAmount = truncatedAmount
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
    }
}