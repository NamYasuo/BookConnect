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
		PagedList<Role> GetAllRole(PagingParams param);
		//int ChangeAccountRole(Guid userId,Guid roleId);
		int DeleteRole(Guid roleId);
    }

}

