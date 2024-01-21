using System;
using APIs.DTO.Ecom;
using APIs.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController: ControllerBase
	{
		private readonly ICartRepository _cartRepo;

		public CartController(ICartRepository cartRepository)
		{
			_cartRepo = cartRepository;
		}

        [HttpPost]
        [Route("add-products-to-cart")]
        public IActionResult AddListProductToCart([FromBody] List<ProductToCartDTO> data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach(ProductToCartDTO d in data)
                    {
                        int result = _cartRepo.AddProductToCart(d.ProductId, d.CartId, d.Quantity);
                    }
                    return Ok();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return BadRequest("Model state unvalid");
        }

        [HttpGet]
		[Route("view-cart-details")]
		public IActionResult ViewCartDetails(Guid userId)
        {
            try
            {
                List<CartDetailsDTO> result = _cartRepo.GetCartDetails(userId);

                if (result.Count == 0) return Ok("Blank cart!");

                else return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



	}
}

