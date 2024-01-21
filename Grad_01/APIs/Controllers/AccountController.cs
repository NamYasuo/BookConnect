using System;
using System.Net.NetworkInformation;
using System.Text;
using APIs.DTO;
using APIs.Services.Intefaces;
using BusinessObjects.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
	{
		private readonly IAccountService accService;

        public AccountController(IAccountService service)
        {
            accService = service;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDTO model)
        {
            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            // check if users exists
            var userExists = accService.FindUserByEmailAsync(model.Email);
            if (userExists.Username != null)
            {
                status.StatusCode = 0;
                status.Message = userExists.Username;
                return Ok(status);
            }
            AppUser user = accService.Register(model);
            return Ok(user);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO model)
        {
            var status = new Status();
            AppUser user = new AppUser();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            user = accService.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            byte[] salt = Convert.FromHexString(user.Salt);
            if (!accService.VerifyPassword(model.Password, user.Password, salt, out byte[] result))
            {
                return BadRequest("Wrong password" + Convert.ToHexString(result));
            }
            string token = accService.CreateToken(user);
            var refreshToken = accService.GenerateRefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiredDate
            };
            Response.Cookies.Append("refreshToken", refreshToken.RefreshToken, cookieOptions);

            //Add refreshtoken to database from Service -> dao
            return Ok(token);

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
                if (accService.GetRoleDetails(data.RoleName) is not null)
                {
                    return BadRequest("Role already existed");
                }
                Role role = new Role()
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = data.RoleName,
                    Description = data.Description
                };

                accService.AddNewRole(role);
                return Ok("New role added");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("get-default-address")]
        public IActionResult GetDefaultAddress(Guid userId)
        {
            try
            {
                Address? address = accService.GetDefaultAddress(userId);
                if (address != null)
                {
                    return Ok(address);
                }
                else return BadRequest("Default Address' not set!!!");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}

