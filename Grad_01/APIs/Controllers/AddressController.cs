using System;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController: ControllerBase
	{
		private readonly IAddressService _addressService;
		public AddressController(IAddressService addressService)
		{
			_addressService = addressService;
		}

		[HttpPost("add-new-address")]
		public IActionResult AddNewAddress([FromBody] NewAddressDTO dto)
		{
			try
			{
				int changes = _addressService.AddNewAddress(new Address
				{
					AddressId = dto.AddressId,
					City_Province = dto.City_Province,
					District = dto.District,
					SubDistrict = dto.SubDistrict,
					Default = dto.Default,
					Rendezvous = dto.Rendezvous,
					UserId = dto.UserId
				});
				IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("No changes!");
				return result;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpGet("get-all-user-address")]
		public IActionResult GetAllUserAddress(Guid userId, [FromQuery] PagingParams @params)
        {
            try
			{
                var addresses = _addressService.GetAllUserAddress(userId, @params);

                if (addresses != null)
                {
                    var metadata = new
                    {
                        addresses.TotalCount,
                        addresses.PageSize,
                        addresses.CurrentPage,
                        addresses.TotalPages,
                        addresses.HasNext,
                        addresses.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(addresses);
                }
                else return BadRequest("No address!!!");
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpPut("update-default-address")]
		public IActionResult UpdateDefaultAddress([FromBody] NewAddressDTO dto)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    Address updateData = new Address
                    {
                        AddressId = dto.AddressId,
                        City_Province = dto.City_Province,
                        District = dto.District,
                        SubDistrict = dto.SubDistrict,
                        Default = dto.Default,
                        Rendezvous = dto.Rendezvous,
                        UserId = dto.UserId
                    };
                    int changes = _addressService.UpdateAddressDefault(updateData);
                    IActionResult result = (changes > 0) ? Ok("Successful!, changes: " + changes) : Ok("No changes!");
                    return result;
                } return BadRequest("Model invalid");
			
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

