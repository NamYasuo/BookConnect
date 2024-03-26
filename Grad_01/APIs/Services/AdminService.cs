using APIs.Services.Interfaces;
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
        private readonly RoleDAO _roleDAO;
        private readonly RoleRecordDAO _roleRecordDAO;
        
        public AdminService()
        { 
            _roleRecordDAO = new RoleRecordDAO();
            _accountDAO = new AccountDAO();
            _roleDAO = new RoleDAO();
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

        public async Task<int> AddNewRole(Role role) => await _roleDAO.AddNewRole(role);

        public async Task<Role?> GetRoleDetails(string roleName) => await _roleDAO.GetRolesDetails(roleName);

        public async Task<int> SetIsBanned(bool choice, Guid userId) => await _accountDAO.SetIsBanned(choice, userId);

        public async Task<int> DeleteRoleAsync(Guid roleId) => await _roleDAO.DeleteRoleAsync(roleId);

        public async Task<PagedList<Role>> GetAllRoleAsync(PagingParams param)
        {
            return  PagedList<Role>.ToPagedList((await _roleDAO.GetAllRoleAsync()).OrderBy(r => r.RoleName).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public async Task<Dictionary<Guid, string>> GetAllUserRolesAsync(Guid userId) => await _roleDAO.GetAllUserRolesAsync(userId);

        public async Task<int> SetUserNewRoleAsync(Guid userId, Guid roleId) => await _roleRecordDAO.AddNewRoleRecordAsync(userId, roleId);

        public async Task<int> RemoveUserRoleAsync(Guid userId, Guid roleId) => await _roleRecordDAO.RemoveRoleRecordAsync(userId, roleId);
    }
}

