﻿using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Base;
using CloudinaryDotNet.Actions;
using DataAccess.DAO;
using DataAccess.DAO.Creative;
using DataAccess.DAO.Ecom;
using Role = BusinessObjects.Models.Role;

namespace APIs.Services
{
    public class AdminService : IAdminService
    {
        private readonly AccountDAO _accountDAO;
        public AdminService()
        {
            _accountDAO = new AccountDAO();
        }

        public int AddBanRecord(BanRecord data) => new BanRecordDAO().AddBanRecord(data);

        public int ForceUnban(Guid userId, string reason) => new BanRecordDAO().ForceUnban(userId, reason);

        public PagedList<Agency> GetAllAgency(PagingParams param)
        {
            return PagedList<Agency>.ToPagedList(new AgencyDAO().GetAllAgency()?.OrderBy(a => a.AgencyName).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public async Task<PagedList<UserProfileDTO>> GetAllUser(PagingParams param)
        {
            List<AppUser> dbResults = await _accountDAO.GetAllUsers();
            List<UserProfileDTO> result = new List<UserProfileDTO>();
            AddressDAO adrDAO = new AddressDAO();
            AccountDAO accDAO = new AccountDAO();

            foreach(AppUser db in dbResults)
            {
                result.Add(new UserProfileDTO
                {
                    UserId = db.UserId,
                    Username = db.Username,
                    //Address = adrDAO.GetUserDefaultAddress(db.UserId).Rendezvous,
                    Email = db.Email,
                    IsBanned = db.IsBanned,
                    //IsSeller = db.IsSeller,
                    IsValidated = db.IsValidated,
                    //Role = accDAO.GetRoleById(db.RoleId).RoleName
                });
            }
            return PagedList<UserProfileDTO>.ToPagedList(result.OrderBy(a => a.Username).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public async Task<int> SetIsBanned(bool choice, Guid userId) => await _accountDAO.SetIsBanned(choice, userId);

        public int DeleteRole(Guid roleId) => new RoleDAO().DeleteRole(roleId);

        public PagedList<Role> GetAllRole(PagingParams param)
        {
            return PagedList<Role>.ToPagedList(new RoleDAO().GetAllRole()?.OrderBy(r => r.RoleName).AsQueryable(), param.PageNumber, param.PageSize);
        }

        //public async Task<int> ChangeAccountRole(Guid userId, Guid roleId) => await _accountDAO.ChangeAccountRole(userId, roleId);
      
    }
}
