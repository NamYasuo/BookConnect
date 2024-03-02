using System;
using APIs.Repositories.Interfaces;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult CreateOrder([FromBody] PaymentReturnDTO data, Guid customerId, Guid addressId)
		{
			Guid orderId = Guid.NewGuid();
			NewOrderDTO dto = new NewOrderDTO()
			{
				OrderId = orderId,
				CustomerId = customerId,
				Status = data.PaymentStatus,
				Notes = data.PaymentMessage,
				PaymentId = Guid.Parse(data.PaymentId),
				AddressId = addressId,
			};
            string result = _orderService.CreateNewOrder(dto);

            if (result == "Successfully!")
			{
                //string result2 = _orderService.TakeProductFromCart(customerId, orderId);

    //            if (result2 == "Successfully!")
				//{
					return Ok("Successfully!");
				//} return BadRequest(result2);
			} return BadRequest(result);
		}

	}
}

