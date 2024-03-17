using System;
using BusinessObjects.Models.Ecom.Base;

namespace APIs.Services.Interfaces
{
	public interface IAdminService
	{
		int SetIsBanned(bool choice, Guid userId);
		int AddBanRecord(BanRecord data);
		int ForceUnban(Guid userId, string reason);
    }
}

