using System;
using APIs.DTO;
using BusinessObjects.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;

namespace APIs.Services.Intefaces
{
    public interface IAccountService
    {
        //User services
        public Task<IdentityResult> ChangePassword(PasswordChangeDTO model);
        public AppUser FindUserByEmailAsync(string email);
        public AppUser Register(RegisterDTO model);
        public bool VerifyPassword(string pwd, string hash, byte[] salt, out byte[] result);
        public string CreateToken(AppUser user);
        public TokenInfo GenerateRefreshToken();
        public void AddNewRole(Role role);
        public Role GetRoleDetails(string roleName);

        //Address services
        public List<Address> GetAllUserAdderess(Guid userId);
        public Address GetDefaultAddress(Guid userId);
        //public AppUser GetUserProfile(Guid userId);
        public void UpdateUserProfile(Guid userId, string username, string? address = null);
        public void UpdateAddress(Guid userId, Guid? addressId, string cityProvince, string district, string subDistrict, string rendezvous, bool isDefault);

        public void RateAndCommentProduct(Guid userId, Guid ratingId, int ratingPoint, string comment);

    }
}

