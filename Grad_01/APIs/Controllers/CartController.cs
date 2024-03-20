using System;
using APIs.DTO.Ecom;
using APIs.Repositories.Interfaces;
using APIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController: ControllerBase
	{
		private readonly ICartRepository _cartRepo;
        private readonly IOrderService _orderService;

		public CartController(ICartRepository cartRepository, IOrderService orderService)
		{
            _orderService = orderService;
			_cartRepo = cartRepository;
		}

        [HttpPost]
        [Route("update-products-to-cart")]
        public IActionResult AddListProductToCart([FromBody] List<ProductToCartDTO> data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach(ProductToCartDTO d in data)
                    {
                        if (_orderService.GetCurrentStock(d.ProductId) < d.Quantity)
                        {
                            return BadRequest("Can't purchase more product than inside stock!" + d.ProductId);
                        }
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

                if (result.Count == 0) return Ok(new List<CartDetailsDTO>());

                else return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("get-cart-id")]
        public IActionResult GetCartId(Guid userId)
        {
            try
            {
                return Ok(_cartRepo.GetUserCartId(userId));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete-product-from-cart")]
        public IActionResult DeleteProductFromCart(Guid productId, Guid cartId, int quantity)
        {
            try
            {
                _cartRepo.DeleteProductFromCart(productId, cartId, quantity);
                return Ok("Product deleted from cart successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

