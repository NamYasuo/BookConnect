using System;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
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
				int changes = _adminService.SetIsBanned(dto.Choice, dto.UserId);
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

