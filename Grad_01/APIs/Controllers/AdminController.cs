using System;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController: ControllerBase
	{
		private readonly IAdminService _adminService;
		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpPost("set-is-account-banned")]
		public IActionResult SetAccountIsBanned(BanUserDTO dto)
		{
			try
			{
				TimeSpan duration = (TimeSpan)((dto.Duration != null) ? dto.Duration : TimeSpan.FromMinutes(15));
				
				int recordChanges = _adminService.AddBanRecord(new BanRecord
				{
					BanRecordId = Guid.NewGuid(),
					BannedDate = DateTime.Now,
					UnbannedDate = DateTime.Now.Add(duration),
					BanReason = dto.Reason,
					UnBanReason = "",
					TargetUserId = dto.UserId
				});
				int accChanges = _adminService.SetIsBanned(true, dto.UserId);
				IActionResult result = (accChanges > 0 && recordChanges > 0) ? Ok("Successful!") : BadRequest("Fail!");
				return result;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpPut("force-unban-account")]
		public IActionResult ForceUnban(BanUserDTO dto)
		{
			try
			{
				int changes = _adminService.ForceUnban(dto.UserId, dto.Reason);
                IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Fail!");
                return result;
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        [HttpGet("get-all-agency")]
        public IActionResult GetAllAgency([FromQuery] PagingParams param)
        {
            try
			{
                var results = _adminService.GetAllAgency(param);

                if (results != null)
                {
                    var metadata = new
                    {
                        results.TotalCount,
                        results.PageSize,
                        results.CurrentPage,
                        results.TotalPages,
                        results.HasNext,
                        results.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(results);
                }
                else return Ok(results);
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        [HttpGet("get-all-user")]
        public IActionResult GetAllUser([FromQuery] PagingParams param)
        {
            try
            {
                var results = _adminService.GetAllUser(param);

                if (results != null)
                {
                    var metadata = new
                    {
                        results.TotalCount,
                        results.PageSize,
                        results.CurrentPage,
                        results.TotalPages,
                        results.HasNext,
                        results.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(results);
                }
                else return Ok(results);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("get-all-role")]
        public IActionResult GetAllRole([FromQuery] PagingParams param)
        {
            try
            {
                var results = _adminService.GetAllRole(param);

                if (results != null)
                {
                    var metadata = new
                    {
                        results.TotalCount,
                        results.PageSize,
                        results.CurrentPage,
                        results.TotalPages,
                        results.HasNext,
                        results.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(results);
                }
                else return Ok(results);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("delete-role")]
        public IActionResult DeleteRole(Guid roleId)
        {
            try
            {
                int changes = _adminService.DeleteRole(roleId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("Fail!");
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("change-user-role")]
        public IActionResult ChangeAccountRole([FromBody] ChangeRoleDTO dto)
        {
            try
            {
                int changes = _adminService.ChangeAccountRole(dto.UserId, dto.RoleId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("Fail!");
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

