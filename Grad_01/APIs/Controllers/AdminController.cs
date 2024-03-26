using System;
using APIs.DTO;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom.Base;
using DataAccess.DTO;
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
		public async Task<IActionResult> SetAccountIsBanned(BanUserDTO dto)
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
				int accChanges = await _adminService.SetIsBanned(true, dto.UserId);
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
        public async Task<IActionResult> GetAllUser([FromQuery] PagingParams param)
        {
            try
            {
                var results = await _adminService.GetAllUser(param);

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
        public async Task<IActionResult> GetAllRoleAsync([FromQuery] PagingParams param)
        {
            try
            {
                var results = await _adminService.GetAllRoleAsync(param);

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

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddNewRole([FromBody] RoleDTO data)
        {
            try
            {
                var status = new Status();
                if (!ModelState.IsValid)
                {
                    status.StatusCode = 0;
                    status.Message = "Please pass all the required fields";
                    return Ok(status);
                }
                if ((await _adminService.GetRoleDetails(data.RoleName)) != null)
                {
                    return BadRequest("Role already existed");
                }
                Role role = new Role()
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = data.RoleName,
                    Description = data.Description
                };

                await _adminService.AddNewRole(role);
                return Ok("New role added");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("delete-role")]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            try
            {
                int changes = await _adminService.DeleteRoleAsync(roleId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("Fail!");
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("get-all-roles-of-user")]
        public async Task<IActionResult> GetAllUserRolesAsync(Guid userId)
        {
            Dictionary<Guid, string> roles = await _adminService.GetAllUserRolesAsync(userId);
            List<Role> results = new List<Role>();
            foreach(var v in roles)
            {
                results.Add(new Role
                {
                    RoleId = v.Key,
                    RoleName = v.Value
                });
            }
            return Ok(results);
        }

        [HttpPost("set-new-role-to-user")]
        public async Task<IActionResult> AddUserNewRoleAsync([FromBody] RoleRecordDTO dto)
        {
            int changes = await _adminService.SetUserNewRoleAsync(dto.UserId, dto.RoleId);
            IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("No changes!");
            return result;
        }

        [HttpDelete("remove-user-from-role")]
        public async Task<IActionResult> RemoveUserRoleAsync([FromBody] RoleRecordDTO dto)
        {
            int changes = await _adminService.RemoveUserRoleAsync(dto.UserId, dto.RoleId);
            IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("No changes!");
            return result;
        }
    }
}

