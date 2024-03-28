using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using APIs.DTO;
using APIs.Services;
using APIs.Services.Interfaces;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Ecom.Rating;
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

        //[HttpPut]
        //[Route("UpdateAddress")]
        //public IActionResult UpdateAddress(Guid userId, Guid? addressId, string cityProvince, string district, string subDistrict, string rendezvous, bool isDefault)
        //{
        //    try
        //    {
        //        // Call the service method to update or create the address
        //        _accService.UpdateAddress(userId, addressId, cityProvince, district, subDistrict, rendezvous, isDefault);

        //        return Ok("Address updated successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"An error occurred: {e.Message}");
        //    }
        //}



        //[HttpPost]
        //[Route("rate-and-comment")]
        //public IActionResult RateAndCommentProduct(Guid userId, Guid ratingId, int ratingPoint, string comment)
        //{
        //    try
        //    {
        //        // Call the service method to rate and comment the product
        //        _accService.RateAndCommentProduct(userId, ratingId, ratingPoint, comment);

        //        return Ok("Product rated and commented successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"An error occurred: {e.Message}");
        //    }
        //}


        [HttpPut("update-user-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromQuery] UserProfile profile)
        {
            try
            {
                string? userpro = _accService.GetUsernameById(profile.UserId);
                string newImgPath = "";
                if (ModelState.IsValid)
                {
                    if (profile.AvatarDir != null)
                    {
                        string? oldImgPath = _accService.GetUserById(profile.UserId)?.AvatarDir;

                        if (oldImgPath != null)
                        {
                            _cloudinaryService.DeleteImage(oldImgPath);
                        }
                        var cloudResponse = _cloudinaryService.UploadImage(profile.AvatarDir, "UserProfile/" + profile.Username + "/Image");
                        if (cloudResponse.StatusCode != 200)
                        {
                            return BadRequest(cloudResponse.Message);
                        }
                        newImgPath = cloudResponse.Data;
                    }
                    AppUser updateData = new AppUser
                    {
                        
                        UserId = profile.UserId,
                        Username = profile.Username,
                        AvatarDir = newImgPath,
                        Email = profile.Email,
                        Phone = profile.Phone,

                    };
                    if (await _accService.UpdateProfile(updateData) > 0)
                    {
                        return Ok("Successful");
                    }
                    return BadRequest("Update fail");
                }
                return BadRequest("Model state invalid");
            }
            catch (Exception e)
            {
                // Include the caught exception's details and the inner exception's details in the message
                string errorMessage = $"An error occurred while updating the user profile: {e.Message}. Inner exception: {e.InnerException?.Message}. Stack trace: {e.StackTrace}";

                // Throw a new exception with the detailed error message
                throw new Exception(errorMessage);
            }


        }
        [HttpGet("get-user-by-id")]
        public IActionResult GetPostById(Guid userId)
        {
            try
            {
                return Ok(_accService.GetUserById(userId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost("rate-and-comment")]
        public IActionResult RateAndCommentProduct([FromForm] RatingRecordDTO ratingRecordDTO)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Check if the user has already rated the product
                    var existingRatingRecord = context.RatingRecords
                        .FirstOrDefault(record => record.UserId == ratingRecordDTO.UserId && record.RatingId == ratingRecordDTO.RatingRecordId);

                    if (existingRatingRecord != null)
                    {
                        // Update existing rating record
                        existingRatingRecord.RatingPoint = ratingRecordDTO.RatingPoint;
                        existingRatingRecord.Comment = ratingRecordDTO.Comment;
                    }
                    else
                    {
                        // Create a new rating record
                        var newRatingRecord = new RatingRecord
                        {
                            RatingRecordId = Guid.NewGuid(),
                            RatingId = ratingRecordDTO.RatingId,
                            UserId = ratingRecordDTO.UserId,
                            RatingPoint = ratingRecordDTO.RatingPoint,
                            Comment = ratingRecordDTO.Comment
                        };
                        _accService.RateAndComment(newRatingRecord);
                    }

                    // Save changes to the database
                    context.SaveChanges();

                    // Calculate the new overall rating for the Rating entity
                    var ratingsForRatingId = context.RatingRecords.Where(record => record.RatingId == ratingRecordDTO.RatingId).ToList();
                    double totalRatingPoints = ratingsForRatingId.Sum(record => record.RatingPoint);
                    double averageRating = ratingsForRatingId.Count > 0 ? totalRatingPoints / ratingsForRatingId.Count : 0;

                    // Update the OverallRating property in the Rating entity
                    var ratingEntity = context.Ratings.FirstOrDefault(r => r.RatingId == ratingRecordDTO.RatingId);
                    if (ratingEntity != null)
                    {
                        ratingEntity.OverallRating = averageRating;
                    }
                    else
                    {
                        // If RatingId not found, create a new Rating entity
                        ratingEntity = new Rating { RatingId = ratingRecordDTO.RatingId, OverallRating = averageRating };
                        context.Ratings.Add(ratingEntity);
                    }

                    // Save changes to the database
                    context.SaveChanges();

                    // Return a success response
                    return Ok("Product rated and commented successfully");
                }
            }
            catch (Exception e)
            {
                // Log the error message along with the inner exception details
                string errorMessage = $"An error occurred while rating and commenting product: {e.Message}. Inner exception: {e.InnerException?.Message}. Stack Trace: {e.StackTrace}";
                Console.WriteLine(errorMessage);

                // Return the error message along with status code 500 and inner exception details
                return StatusCode(500, errorMessage);
            }
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

