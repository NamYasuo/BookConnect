﻿using System;
using System.Linq;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Rating;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class AccountDAO
	{
        /*---------------------------------------APPUSER-------------------------------------------*/
        /*------------------BEGIN GET-------------------*/

        public List<AppUser> GetAllUsers()
        {
            List<AppUser> listUser = new List<AppUser>();
            try
            {
                using (var context = new AppDbContext())
                {
                    listUser = context.AppUsers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listUser;
        }

        public string GetAccountSalt(Guid userId)
        {
            string salt;
            try
            {
                using (var context = new AppDbContext())
                {
                    salt = context.Database.SqlQuery<string>($"select Salt from AppUser where UserId = {userId}").ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return salt;
        }

        public Role GetRolesDetails(string roleName)
        {
            Role? role = new Role();
            try
            {
                using (var context = new AppDbContext())
                {
                    role = context.Roles.Where(r => r.RoleName == roleName).FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return role;
        }

        public AppUser? FindUserByEmailAsync(string email)
        {
            AppUser? user = null;
            try
            {
                using (var context = new AppDbContext())
                {
                    if (CheckEmail(email))
                    {
                        user = context.AppUsers.Where(u => u.Email.Equals(email)).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public string? GetNameById(Guid userId)
        {
            try
            {
                string? result = "";
                using (var context = new AppDbContext())
                {
                    result = context.AppUsers.Where(u => u.UserId == userId).FirstOrDefault()?.Username;
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Role GetRoleById(Guid roleId)
        {
            try
            {
                Role result;
                using (var context = new AppDbContext())
                {
                    Role? dbResult = context.Roles.Where(r => r.RoleId == roleId).SingleOrDefault();
                    result = (dbResult != null) ? dbResult : new Role();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public List<AppUser> GetUserByRole()
        /*------------------END GET-------------------*/

        /*------------------BEGIN CHECK-------------------*/

        //Check if username existed, if existed return true else return false
        public bool CheckUsername(string username)
        {
            bool result = false;
            try
            {
                using (var context = new AppDbContext())
                {
                    context.AppUsers.Any(a => a.Username == username);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //Check if email existed, if existed return true else return false
        public bool CheckEmail(string email)
        {
            bool result = false;
            try
            {
                using (var context = new AppDbContext())
                {
                    result = context.AppUsers.Any(a => a.Email == email);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public bool IsUserValidated(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        return user.IsValidated;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsSeller(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        return user.IsSeller;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> IsBanned(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    DateTime? latestUnbannedDate = context.BanRecords
                    .Where(r => r.TargetUserId == userId)
                    .OrderByDescending(r => r.UnbannedDate)
                    .Select(r => r.UnbannedDate)
                    .FirstOrDefault();

                    if (latestUnbannedDate < DateTime.Now)
                    {
                        AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                        if (user != null)
                        {
                            user.IsBanned = false;
                            await context.SaveChangesAsync();
                        }
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*-------------------END CHECK-------------------*/


        /*----------------BEGIN POST----------------------*/

        public void CreateAccount(AppUser user)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    if (context.AppUsers.ToList().Count() == 0)
                    {
                        context.AppUsers.Add(user);
                        context.SaveChanges();
                    }
                    if (user.Email != null && !CheckEmail(user.Email) && !CheckUsername(user.Username))
                    {
                        context.AppUsers.Add(user);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddNewRole(Role role)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*-----------------END POST-------------------*/


        /*---------------BEGIN PUT-------------------*/
        public int ChangeAccountRole(Guid userId, Guid roleId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? record = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (record != null)
                    {
                        record.RoleId = roleId;
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetIsAccountValid(bool choice, Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        user.IsValidated = choice;
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetIsAgency(bool choice, Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        user.IsSeller = choice;
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetIsBanned(bool choice, Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        user.IsBanned = choice;
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
        public void UpdateUsernameAndAddress(Guid userId, string username, string cityProvince, string district, string subDistrict, string rendezvous, bool isDefault)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Update user profile
                    UpdateUserProfile(userId, username);

                    // Find existing address
                    var existingAddress = context.Addresses.FirstOrDefault(a => a.UserId == userId);

                    if (existingAddress != null)
                    {
                        // Update existing address
                        existingAddress.City_Province = cityProvince;
                        existingAddress.District = district;
                        existingAddress.SubDistrict = subDistrict;
                        existingAddress.Rendezvous = rendezvous;
                        existingAddress.Default = isDefault;
                    }
                    else
                    {
                        // Create new address
                        var newAddress = new Address
                        {
                            AddressId = Guid.NewGuid(),
                            UserId = userId,
                            City_Province = cityProvince,
                            District = district,
                            SubDistrict = subDistrict,
                            Rendezvous = rendezvous,
                            Default = isDefault
                        };

                        context.Addresses.Add(newAddress);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error updating username and address: " + e.Message);
            }
        }


















    }
}

