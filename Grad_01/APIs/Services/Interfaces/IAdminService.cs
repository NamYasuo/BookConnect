using System;
using APIs.Utils.Paging;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom.Base;

namespace APIs.Services.Interfaces
{
	public interface IAdminService
	{
		int SetIsBanned(bool choice, Guid userId);
		int AddBanRecord(BanRecord data);
		int ForceUnban(Guid userId, string reason);
        PagedList<Agency> GetAllAgency(PagingParams param);
    }

}

