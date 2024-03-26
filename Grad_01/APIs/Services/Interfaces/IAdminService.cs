using System;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom.Base;

namespace APIs.Services.Interfaces
{
	public interface IAdminService
	{
        Task<int> SetIsBanned(bool choice, Guid userId);
		int AddBanRecord(BanRecord data);
		int ForceUnban(Guid userId, string reason);
        PagedList<Agency> GetAllAgency(PagingParams param);
        Task<PagedList<UserProfileDTO>> GetAllUser(PagingParams param);
        Task<int> AddNewRole(Role role);
        Task<Role?> GetRoleDetails(string roleName);
        Task<PagedList<Role>> GetAllRoleAsync(PagingParams param);
        Task<int> DeleteRoleAsync(Guid roleId);

        Task<Dictionary<Guid, string>> GetAllUserRolesAsync(Guid userId);
        Task<int> SetUserNewRoleAsync(Guid userId, Guid roleId);
        Task<int> RemoveUserRoleAsync(Guid userId, Guid roleId);
    }

}

