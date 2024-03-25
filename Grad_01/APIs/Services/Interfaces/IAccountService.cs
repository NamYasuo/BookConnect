using System;
using APIs.DTO;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Identity;

namespace APIs.Services.Interfaces
{
    public interface IAccountService
    {
        //User services
        Task<IdentityResult> ChangePassword(PasswordChangeDTO model);
        Task<AppUser?> FindUserByEmailAsync(string email);
        Task<AppUser> Register(RegisterDTO model);
        bool VerifyPassword(string pwd, string hash, byte[] salt);
        string CreateToken(AppUser user);
        Task<RefreshToken?> GenerateRefreshTokenAsync(Guid userId);
        Task<int> AddNewRole(Role role);
        Task<Role?> GetRoleDetails(string roleName);
        Task<string?> GetUsernameById(Guid userId);
        Task<bool> IsBanned(Guid userId);
        Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
        Task<AppUser?> FindUserByIdAsync(Guid userId);

        //Address services
        List<Address> GetAllUserAdderess(Guid userId);
        Address GetDefaultAddress(Guid userId);

        //Validate services
        Task<int> SetUserIsValidated(bool choice, Guid userId);
        Task<bool> IsUserValidated(Guid userId);

        //Agency registration
        List<Agency> GetOwnerAgencies(Guid ownerId);
        string RegisterAgency(AgencyRegistrationDTO dto, string logoUrl);
        //bool IsSeller(Guid userId);
        Agency GetAgencyById(Guid agencyId);
        int UpdateAgency(AgencyUpdateDTO updatedData, string? logoUrl);
    }
}

