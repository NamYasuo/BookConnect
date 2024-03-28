using System;
using APIs.DTO;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Ecom.Rating;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;

namespace APIs.Services.Interfaces
{
    public interface IAccountService
    {
        //User services
        Task<IdentityResult> ChangePassword(PasswordChangeDTO model);
        AppUser? FindUserByEmailAsync(string email);
        AppUser Register(RegisterDTO model);
        bool VerifyPassword(string pwd, string hash, byte[] salt, out byte[] result);
        string CreateToken(AppUser user);
        TokenInfo GenerateRefreshToken();
        void AddNewRole(Role role);
        Role GetRoleDetails(string roleName);
        string? GetUsernameById(Guid userId);
        Task<bool> IsBanned(Guid userId);

        //Address services
        List<Address> GetAllUserAdderess(Guid userId);
        Address GetDefaultAddress(Guid userId);

        //Validate services
        int SetUserIsValidated(bool choice, Guid userId);
        bool IsUserValidated(Guid userId);

        //Agency registration
        List<Agency> GetOwnerAgencies(Guid ownerId);
        string RegisterAgency(AgencyRegistrationDTO dto, string logoUrl);
        bool IsSeller(Guid userId);
        Agency GetAgencyById(Guid agencyId);
        int UpdateAgency(AgencyUpdateDTO updatedData, string? logoUrl);

        public  Task<int> UpdateProfile(AppUser appUser);
        public AppUser? GetUserById(Guid userId);
        public int RateAndComment(RatingRecord ratingRecord);
    }
}

