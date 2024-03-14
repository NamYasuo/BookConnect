using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using APIs.DTO;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
	{
		private readonly IAccountService _accService;

        public AccountController(IAccountService service)
        {
            _accService = service;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp([FromBody] RegisterDTO model)
        {
            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            // check if users exists
            var userExists = _accService.FindUserByEmailAsync(model.Email);
            if (userExists != null && userExists?.Username != null)
            {
                status.StatusCode = 0;
                status.Message = userExists.Username;
                return Ok(status);
            }
            AppUser user = _accService.Register(model);
            return Ok(user);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO model)
        {
            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            AppUser? user = _accService.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            byte[] salt = Convert.FromHexString(user.Salt);
            if (!_accService.VerifyPassword(model.Password, user.Password, salt, out byte[] result))
            {
                return BadRequest("Wrong password");
            }
            string token = _accService.CreateToken(user);
            var refreshToken = _accService.GenerateRefreshToken();

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
                if (_accService.GetRoleDetails(data.RoleName) is not null)
                {
                    return BadRequest("Role already existed");
                }
                Role role = new Role()
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = data.RoleName,
                    Description = data.Description
                };

                _accService.AddNewRole(role);
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
                Address? address = _accService.GetDefaultAddress(userId);
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

        [HttpPost]
        [Route("upload-cic")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files, Guid userId)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        [HttpGet, Authorize]
        [Route("get-user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role);
                var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
                var emailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
                if (userIdClaim != null)
                {
                    var userId = Guid.Parse(userIdClaim.Value);
                    if (roleClaim != null)
                    {
                        if (usernameClaim != null)
                        {
                            Address? address = _accService.GetDefaultAddress(userId);
                            string rendez = string.Empty;

                            if(address != null && address.Rendezvous != null)
                            {
                                rendez = address.Rendezvous;
                            }

                            UserProfile profile = new UserProfile()
                            {
                                UserId = userId,
                                Username = usernameClaim.Value,
                                Role = roleClaim.Value,
                                Address = rendez,
                                Email = emailClaim.Value
                            };
                            return Ok(profile);
                        }
                        else return BadRequest("Username claim not found!!!");
                    } else return BadRequest("Role claim not found!!!");
                } else return BadRequest("User ID claim not found!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("set-is-account-validated")]
        public IActionResult SetIsAccountValid([FromBody] UserValidationDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = _accService.SetUserIsValidated(dto.choice, dto.userId);
                    IActionResult apiResult = (result > 0) ? Ok("Successful!") : BadRequest("No change!");
                    return apiResult;
                }
                return BadRequest("Model invalid");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

