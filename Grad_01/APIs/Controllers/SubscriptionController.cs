using System;
using APIs.Services.Interfaces;
using BusinessObjects.DTO.Subscription;
using BusinessObjects.Models.Creative;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController: ControllerBase
	{
		private readonly ISubscriptionService _subService;

        public SubscriptionController(ISubscriptionService subService)
        {
            _subService = subService;
        }

        [HttpPost("add-subscription-tier")]
        public IActionResult AddNewTier([FromBody] NewTierDTO dto)
        {
            try
            {
                Tier newTier = new Tier()
                {
                    CreatorId = dto.CreatorId,
                    TierId = Guid.NewGuid(),
                    Price = dto.Price,
                    Duration = dto.Duration,
                    Status = "active",
                    TierType = dto.TierType
                };
               int result = _subService.AddNewTier(newTier);
                if (result > 0) return Ok("Successful!");
                else return BadRequest("Fail to add!");
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        [HttpGet("get-tiers-by-userId")]
        public IActionResult GetTierById(Guid userId)
        {
            try
            {
                return Ok(_subService.GetTierListByUserId(userId));
                    
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}

