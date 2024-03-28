﻿using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using APIs.DTO;
using APIs.Services.Intefaces;
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

        private readonly ICloudinaryService _cloudinaryService;

        public AccountController(IAccountService service, ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
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
                return BadRequest(status);
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


            if(!user.IsBanned)

            {
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
            return BadRequest("Account is banned!");
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

            catch (Exception e)

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

            catch (Exception e)

            {
                throw new Exception(e.Message);
            }
        }

        //[HttpPost]
        //[Route("upload-cic")]
        //public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files, Guid userId)
        //{
        //    long size = files.Sum(f => f.Length);

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var filePath = Path.GetTempFileName();

        //            using (var stream = System.IO.File.Create(filePath))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    // Process uploaded files
        //    // Don't rely on or trust the FileName property without validation.

        //    return Ok(new { count = files.Count, size });
        //}

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
                                    Role = roleClaim.Value,
                                    Address = rendez,
                                    Email = emailClaim.Value,
                                    IsValidated = _accService.IsUserValidated(userId),
                                    IsSeller = _accService.IsSeller(userId),
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

            catch (Exception e)

            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("register-agency")]
        public IActionResult RegisterAgency([FromForm] AgencyRegistrationDTO dto)
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

                    if (_accService.IsUserValidated(userId) == true)
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

        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(Guid userId, Guid? addressId, string cityProvince, string district, string subDistrict, string rendezvous, bool isDefault)
        {
            try
            {
                // Call the service method to update or create the address
                _accService.UpdateAddress(userId, addressId, cityProvince, district, subDistrict, rendezvous, isDefault);

                return Ok("Address updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }



        [HttpPost]
        [Route("rate-and-comment")]
        public IActionResult RateAndCommentProduct(Guid userId, Guid ratingId, int ratingPoint, string comment)
        {
            try
            {
                // Call the service method to rate and comment the product
                _accService.RateAndCommentProduct(userId, ratingId, ratingPoint, comment);

                return Ok("Product rated and commented successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }


        [HttpPost("updateUsernameAndAddress")]
        public IActionResult UpdateUsernameAndAddress([FromBody] UserProfile request)
        {
            try
            {
                _accService.UpdateUsernameAndAddress(request.UserId, request.Username, request.CityProvince, request.District, request.SubDistrict, request.Rendezvous, request.IsDefault);
                return Ok("Username and address updated successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error updating username and address: " + e.Message);
            }
        }
        //[HttpGet, Authorize]
        //[Route("get-user-profile")]
        //public async Task<IActionResult> GetUserProfile()
        //{
        //    try
        //    {
        //        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
        //        var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role);
        //        var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
        //        var emailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
        //        if (userIdClaim != null)
        //        {
        //            var userId = Guid.Parse(userIdClaim.Value);
        //            if (roleClaim != null)
        //            {
        //                if (usernameClaim != null)
        //                {
        //                  Address address = accService.GetDefaultAddress(userId);
        //                    UserProfile profile = new UserProfile()
        //                    {
        //                        UserId = userId,
        //                        Username = usernameClaim.Value,
        //                        Role = roleClaim.Value,
        //                        Address = address.Rendezvous,
        //                        Email = emailClaim.Value
        //                    };
        //                    return Ok(profile);
        //                }
        //                else return BadRequest("Username claim not found!!!");
        //            } else return BadRequest("Role claim not found!!!");
        //        } else return BadRequest("User ID claim not found!!!");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

    }
}

