using System;
using APIs.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
	{
		public IBookRepository _bookRepo;

		public OrderController(IBookRepository bookRepo)
		{
			_bookRepo = bookRepo;
		}

		//View items in cart
		[HttpGet]
		[Route("get-items-in-cart")]
		public IActionResult GetItemsInCart([FromBody] List<string> bookIds)
		{
                List<Guid> guids = new List<Guid>();
                foreach (string s in bookIds)
                {
                    guids.Add(Guid.Parse(s));
                }
                try
                {
                    return Ok(_bookRepo.GetBookListById(guids));
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
		}


		//
		//Create order (CartToOrderDTO)
		//

	}
}

