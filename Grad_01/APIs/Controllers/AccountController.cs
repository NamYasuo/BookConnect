using System.Security.Claims;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
	{
		private readonly IAccountService _accService;
        private readonly ICloudinaryService _cloudinaryService;

        public AccountController(IAccountService service, ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
            _accService = service;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDTO model)
        {
            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return BadRequest(status);
            }
            // check if users exists
            var userExists = await _accService.FindUserByEmailAsync(model.Email);
            if (userExists != null && userExists?.Username != null)
            {
                status.StatusCode = 0;
                status.Message = userExists.Username;
                return Ok(status);
            }
            AppUser user = await _accService.Register(model);
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
            AppUser? user = await _accService.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
              return Unauthorized("User not found!");
            }

            if(!user.IsBanned)
            {
                byte[] salt = Convert.FromHexString(user.Salt);
                if (!_accService.VerifyPassword(model.Password, user.Password, salt))
                {
                    return Unauthorized("Wrong password");
                }
                string accessToken = await _accService.CreateTokenAsync(user);
                var refreshToken = await _accService.GenerateRefreshTokenAsync(user.UserId);

                if (refreshToken == null) return BadRequest("Fail to create refresh token");

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.ExpiredDate,
                    Secure = true 
                };
                Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

                return Ok(accessToken);
            }
            return BadRequest("Account is banned!");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshTokenRequest)
        {
            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return BadRequest(status);
            }

            var refreshToken = await _accService.ValidateRefreshTokenAsync(refreshTokenRequest);

            if (refreshToken == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            if (refreshToken.ExpiredDate < DateTime.Now)
            {
                return Unauthorized("Refresh token has expired");
            }

            var user = await _accService.FindUserByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                return Unauthorized("Invalid user");
            }

            var accessToken = await _accService.CreateTokenAsync(user);

            var newRefreshToken = await _accService.GenerateRefreshTokenAsync(user.UserId);

            if (newRefreshToken == null) return BadRequest("Fail to generate refresh token!");

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpiredDate,
                Secure = true 
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            return Ok(new { AccessToken = accessToken });
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

        [HttpGet, Authorize]
        [Route("get-user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                var roleClaim1 = HttpContext.User.FindFirst(ClaimTypes.Role);
                var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
                var emailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
                List<string> roles = new List<string>();

                var roleClaims = HttpContext.User.FindAll(ClaimTypes.Role);

                if (userIdClaim != null)
                {
                    var userId = Guid.Parse(userIdClaim.Value);
                    if (roleClaims.Count() > 0)
                    {
                        foreach(var r in roleClaims)
                        {
                            roles.Add(r.Value);
                        }
                        if (usernameClaim != null)
                        {
                            if (emailClaim != null)
                            {
                                Address? address = _accService.GetDefaultAddress(userId);
                                string rendez = string.Empty;

                                if (address != null && address.Rendezvous != null)
                                {
                                    rendez = address.Rendezvous;
                                }


                                UserProfileDTO profile = new UserProfileDTO()
                                {
                                    UserId = userId,
                                    Username = usernameClaim.Value,
                                    Roles = roles,
                                    Address = rendez,
                                    Email = emailClaim.Value,
                                    IsValidated = await _accService.IsUserValidated(userId),
                                    //IsSeller = _accService.IsSeller(userId),
                                    IsBanned = await _accService.IsBanned(userId),
                                    Agencies = _accService.GetOwnerAgencies(userId)
                                };
                                return Ok(profile);
                            }
                            else return NotFound("Email claim not found!");
                        }
                        else return NotFound("Username claim not found!!!");
                    } else return NotFound("Role claim not found!!!");
                } else return NotFound("User ID claim not found!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("set-is-account-validated")]
        public async Task<IActionResult> SetIsAccountValid([FromBody] UserValidationDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = await _accService.SetUserIsValidated(dto.choice, dto.userId);
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

        [HttpPost("register-agency")]
        public async Task<IActionResult> RegisterAgency([FromForm] AgencyRegistrationDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");

                    string logoUrl = "";

                    Guid userId = (userIdClaim != null) ? Guid.Parse(userIdClaim.Value) : Guid.Empty;

                    if (dto.LogoImg != null)
                    {
                        CloudinaryResponseDTO cldRspDTO = _cloudinaryService.UploadImage(dto.LogoImg, "Agencies/" + dto.AgencyName + "/Logo");
                        logoUrl = (cldRspDTO.StatusCode == 200 && cldRspDTO.Data != null)
                      ? cldRspDTO.Data : "";
                    }

                    if (await _accService.IsUserValidated(userId))
                    {
                        string result = _accService.RegisterAgency(dto, logoUrl);
                        if (result == "Successful!")
                        {
                            return Ok(result);
                        }
                        else return BadRequest(result);
                    }
                    else return BadRequest("Account's not validated!");
                }
                else return BadRequest("Model invalid!");
               
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("get-agency-by-id")]
        public IActionResult GetAgencyById(Guid agencyId)
        {
            try
            {
                Agency result = _accService.GetAgencyById(agencyId);
                IActionResult response = (result.AgencyName != null) ? Ok(result) : NotFound("Agency doesn't exist!");
                return response;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("update-agency")]
        public IActionResult UpdateAgency([FromForm] AgencyUpdateDTO dto)
        {
            string? logoUrl = null;

            if (dto.LogoImg != null)
            {
                CloudinaryResponseDTO cldRspDTO = _cloudinaryService.UploadImage(dto.LogoImg, "Agencies/" + dto.AgencyName + "/Logo");
                logoUrl = (cldRspDTO.StatusCode == 200 && cldRspDTO.Data != null)
              ? cldRspDTO.Data : null;
            }

            int changes = _accService.UpdateAgency(dto, logoUrl);
            IActionResult response = (changes > 0) ? Ok("Successful!") : Ok("No changes");
            return response;
        }
    }
}

