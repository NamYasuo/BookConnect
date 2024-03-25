using System;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Rating;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class AccountDAO
	{
        private readonly AppDbContext _context;
        public AccountDAO()
        {
            _context = new AppDbContext();
        }
        /*---------------------------------------APPUSER-------------------------------------------*/
        /*------------------BEGIN GET-------------------*/

        public async Task<List<AppUser>> GetAllUsers() => await _context.AppUsers.ToListAsync();
       
        public async Task<string?> GetAccountSalt(Guid userId)
        {
            AppUser? user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
            string? salt = (user != null) ? user.Salt : null;
            return salt;
        }

        public async Task<Role?> GetRolesDetails(string roleName)
        => await _context.Roles.SingleOrDefaultAsync(r => r.RoleName.Equals(roleName));

        public async Task<AppUser?> FindUserByEmailAsync(string email)
        {
            AppUser? user =  (await CheckEmail(email)) ? await _context.AppUsers.SingleOrDefaultAsync(u => u.Email == email) : null;
            return user;
        }

        public async Task<string?> GetNameById(Guid userId)
        {
            AppUser? user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
            string? name = (user != null) ? user.Username : null;
            return name;
        }

        public async Task<Role?> GetRoleById(Guid roleId) => await _context.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId);

        public async Task<AppUser?> FindUserByIdAsync(Guid userId) => await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
        /*------------------END GET-------------------*/

        /*------------------BEGIN CHECK-------------------*/

        //Check if username existed, if existed return true else return false
        public async Task<bool> CheckUsername(string username) => await _context.AppUsers.AnyAsync(a => a.Username == username);

        //Check if email existed, if existed return true else return false
        public async Task<bool> CheckEmail(string email) => await _context.AppUsers.AnyAsync(a => a.Email == email);

        public async Task<bool> IsUserValidated(Guid userId)
        {
            AppUser? user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
            bool result = (user != null) ? user.IsValidated : false;
            return result;
        }

        public async Task<bool> IsBanned(Guid userId)
        {
                    DateTime? latestUnbannedDate = _context.BanRecords
                    .Where(r => r.TargetUserId == userId)
                    .OrderByDescending(r => r.UnbannedDate)
                    .Select(r => r.UnbannedDate)
                    .FirstOrDefault();

                    if(latestUnbannedDate < DateTime.Now)
                    {
                        AppUser? user = _context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                        if (user != null)
                        {
                            user.IsBanned = false;
                            await _context.SaveChangesAsync();
                        }
                        return false;
                    }
                    return true;
        }

        /*-------------------END CHECK-------------------*/


        /*----------------BEGIN POST----------------------*/

        public async Task<int> CreateAccount(AppUser user)
        {
            if (!_context.AppUsers.Any())
            {
                await _context.AppUsers.AddAsync(user);
            }
            else if (user.Email != null && !(await CheckEmail(user.Email)) && !(await CheckUsername(user.Username)))
            {
                await _context.AppUsers.AddAsync(user);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddNewRole(Role role)
        {
           await _context.Roles.AddAsync(role);
           return await _context.SaveChangesAsync();
        }

        /*-----------------END POST-------------------*/


        /*---------------BEGIN PUT-------------------*/
        public async Task<int> SetIsAccountValid(bool choice, Guid userId)
        {
              AppUser? user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
               if (user != null)
               {
                 user.IsValidated = choice;
               }
               return await _context.SaveChangesAsync();
        }

        public async Task<int> SetIsBanned(bool choice, Guid userId)
        {
            AppUser? user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsBanned = choice;
            }
            return await _context.SaveChangesAsync();
        }
        /*-----------------END -------------------*/


        /*----------------BEGIN DELETE-----------------------*/

        /*-----------------END DELETE-------------------*/

        /*------------------------------------END APPUSER-------------------------------------------*/
        public void UpdateUserProfile(Guid userId, string username, string? address = null)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Find the user by user ID
                    var user = context.AppUsers.FirstOrDefault(u => u.UserId == userId);

                    if (user != null)
                    {
                        // Update username if provided
                        if (username != null)
                        {
                            user.Username = username;
                        }

                        // Address update (using address string)
                        if (address != null)
                        {

                        }

                        context.AppUsers.Update(user);
                        context.SaveChanges();
                    }
                    else
                    {
                        // Throw an exception or handle the case where user not found
                        throw new Exception("User not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void RateAndCommentProduct(Guid userId, Guid ratingId, int ratingPoint, string comment)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Check if the user has already rated the product
                    var existingRatingRecord = context.RatingRecords
                        .FirstOrDefault(record => record.UserId == userId && record.RatingId == ratingId);

                    if (existingRatingRecord != null)
                    {
                        // Update existing rating record
                        existingRatingRecord.RatingPoint = ratingPoint;
                        existingRatingRecord.Comment = comment;
                    }
                    else
                    {
                        // Create a new rating record
                        var newRatingRecord = new RatingRecord
                        {
                            RatingId = ratingId,
                            UserId = userId,
                            RatingPoint = ratingPoint,
                            Comment = comment
                        };
                        context.RatingRecords.Add(newRatingRecord);
                    }

                    // Calculate the new overall rating for the Rating entity
                    var ratingsForRatingId = context.RatingRecords.Where(record => record.RatingId == ratingId).ToList();
                    double totalRatingPoints = ratingsForRatingId.Sum(record => record.RatingPoint);
                    double averageRating = ratingsForRatingId.Count > 0 ? totalRatingPoints / ratingsForRatingId.Count : 0;

                    // Update the OverallRating property in the Rating entity
                    var ratingEntity = context.Ratings.FirstOrDefault(r => r.RatingId == ratingId);
                    if (ratingEntity != null)
                    {
                        ratingEntity.OverallRating = averageRating;
                    }
                    else
                    {
                        // If RatingId not found, create a new Rating entity
                        ratingEntity = new Rating { RatingId = ratingId, OverallRating = averageRating };
                        context.Ratings.Add(ratingEntity);
                    }

                    // Save changes to the database
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                // Log the inner exception details along with the message
                string errorMessage = $"Error rating and commenting product: {e.Message}";
                if (e.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {e.InnerException.Message}";
                }
                throw new Exception(errorMessage);
            }

        }

       }
}

