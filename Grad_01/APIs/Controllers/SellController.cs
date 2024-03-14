//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using APIs.DTO;
//using APIs.Repositories.Interfaces;
//using BusinessObjects.Models;
//using BusinessObjects.Models.Ecom;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace APIs.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SellerController : ControllerBase
//    {
//        private readonly ISellRepository _sellRepository;

//        public SellerController(ISellRepository sellRepository)
//        {
//            _sellRepository = sellRepository;
//        }

//        [HttpPost("CreateListing")]
//        public IActionResult CreateListing([FromBody] Book book)
//        {
//            try
//            {
//                _sellRepository.CreateBookListing(book);
//                return Ok();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpPut("UpdateListing")]
//        public IActionResult UpdateListing([FromBody] Book book)
//        {
//            try
//            {
//                _sellRepository.UpdateBookListing(book);
//                return Ok();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpDelete("DeleteListing/{bookId}")]
//        public IActionResult DeleteListing(Guid bookId)
//        {
//            try
//            {
//                _sellRepository.DeleteBookListing(bookId);
//                return Ok();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpGet("GetAllListings")]
//        public IActionResult GetAllListings()
//        {
//            try
//            {
//                var listings = _sellRepository.GetAllBookListings();
//                return Ok(listings);
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpPost("ProcessOrder")]
//        public IActionResult ProcessOrder([FromBody] Order order)
//        {
//            try
//            {
//                _sellRepository.ProcessOrder(order);
//                return Ok();
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//    }
//}

