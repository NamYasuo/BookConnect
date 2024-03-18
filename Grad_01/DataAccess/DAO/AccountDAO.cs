using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Base;
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

                    if(latestUnbannedDate < DateTime.Now)
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

























    }
}

