using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIs.DTO;
using APIs.Repositories.Interfaces;
using APIs.Services;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using DataAccess.DAO;
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
        public IActionResult AddBookListing([FromBody] BookListingManageDTOs item)
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
        public IActionResult UpdateBookListing([FromBody] BookListingManageDTOs item)
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

        [HttpGet("GetBookListingById/{Id}")]
        public IActionResult GetBookListingBySellerId(Guid Id)
        {
            try
            {
                var items = _sellRepository.GetBookListingById(Id);
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
        public IActionResult AddInventoryItem([FromBody] InventoryManageDTOs item)
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
        public IActionResult UpdateInventoryItem([FromBody] InventoryManageDTOs item)
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

        [HttpGet("GetInventoryItemsById/{Id}")]
        public IActionResult GetInventoryItemsBySellerId(Guid Id)
        {
            try
            {
                var items = _sellRepository.GetInventoryItemsById(Id);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // Ads Endpoints

        [HttpPost("AddAds")]
        public IActionResult AddAds([FromBody] AdsManageDTOs item)
        {
            try
            {
                _sellRepository.AddToAds(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateAds")]
        public IActionResult UpdateAds([FromBody] AdsManageDTOs item)
        {
            try
            {
                _sellRepository.UpdateAds(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteAds/{itemId}")]
        public IActionResult DeleteAds(Guid itemId)
        {
            try
            {
                _sellRepository.RemoveFromAds(itemId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAdsById/{Id}")]
        public IActionResult GetAdsById(Guid Id)
        {
            try
            {
                var items = _sellRepository.GetAdsById(Id);
                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // Communicate Endpoints
        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody] MessageDTOs message)
        {
            try
            {
                _sellRepository.SendMessage(message);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetMessages/{senderId}/{receiverId}")]
        public IActionResult GetMessages(Guid senderId, Guid receiverId)
        {
            try
            {
                var messages = _sellRepository.GetMessages(senderId, receiverId);
                return Ok(messages);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
