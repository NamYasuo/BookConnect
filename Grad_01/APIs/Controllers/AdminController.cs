using System;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models.Ecom.Base;
using Microsoft.AspNetCore.Mvc;

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
	}
}

