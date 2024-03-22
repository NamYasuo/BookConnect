using System;
using APIs.Services;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Subscription;
using BusinessObjects.Models.Creative;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult GetTierByCreatorId(Guid userId)
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

        [HttpPost("add-new-sub")]
        public IActionResult AddNewSub([FromForm] NewSubDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_subService.IsCreatorOfTier(dto.SubcriberId, dto.TierId))
                    {
                        DateTime startDate = DateTime.Now;
                        int tierDuration = _subService.GetTierDurationById(dto.TierId);
                        Subscription newSub = new Subscription()
                        {
                            SubId = Guid.NewGuid(),
                            SubscriberId = dto.SubcriberId,
                            TierId = dto.TierId,
                            StartDate = startDate,
                            EndDate = startDate.AddDays(tierDuration),
                            Status = "active",
                        };
                        int result = _subService.AddNewSub(newSub);
                        if (result > 0) return Ok("Successful!");
                        else return BadRequest("Sub fail!");
                    }
                    else return BadRequest("Can't sub to your own tier");
                }
                else return BadRequest("Model invalid!");

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("check-out")]
        public async Task<IActionResult> CheckoutAsync([FromBody] PreSubCheckoutDTO dto)
        {
            int truncatedAmount = (int)Math.Round(dto.Price, MidpointRounding.AwayFromZero);

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
        }

        }
    }

