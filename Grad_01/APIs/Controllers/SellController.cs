using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIs.DTO;
using APIs.Repositories.Interfaces;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SellerController : ControllerBase
    {
        private readonly ISellRepository _sellRepository;

        public SellerController(ISellRepository sellRepository)
        {
            _sellRepository = sellRepository;
        }
        // Book Listings Endpoints

        [HttpPost("AddBookListing")]
        public IActionResult AddBookListing([FromBody] BookListing item)
        {
            try
            {
                _sellRepository.AddToBookListing(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateBookListing")]
        public IActionResult UpdateBookListing([FromBody] BookListing item)
        {
            try
            {
                _sellRepository.UpdateBookListing(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteBookListing/{itemId}")]
        public IActionResult DeleteBookListing(Guid itemId)
        {
            try
            {
                _sellRepository.RemoveFromBookListing(itemId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetBookListingBySellerId/{sellerId}")]
        public IActionResult GetBookListingBySellerId(Guid sellerId)
        {
            try
            {
                var items = _sellRepository.GetBookListingBySellerId(sellerId);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetBookListingByName/{listName}")]
        public IActionResult GetBookListingByName(string listName)
        {
            try
            {
                var items = _sellRepository.GetBookListingByName(listName);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Inventory Endpoints

        [HttpPost("AddInventoryItem")]
        public IActionResult AddInventoryItem([FromBody] Inventory item)
        {
            try
            {
                _sellRepository.AddToInventory(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateInventoryItem")]
        public IActionResult UpdateInventoryItem([FromBody] Inventory item)
        {
            try
            {
                _sellRepository.UpdateInventoryItem(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteInventoryItem/{itemId}")]
        public IActionResult DeleteInventoryItem(Guid itemId)
        {
            try
            {
                _sellRepository.RemoveFromInventory(itemId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetInventoryItemsBySellerId/{sellerId}")]
        public IActionResult GetInventoryItemsBySellerId(Guid sellerId)
        {
            try
            {
                var items = _sellRepository.GetInventoryItemsBySellerId(sellerId);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
