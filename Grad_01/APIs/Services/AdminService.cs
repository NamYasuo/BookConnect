using System;
using APIs.Services.Interfaces;
using BusinessObjects.Models.Ecom.Base;
using DataAccess.DAO;

namespace APIs.Services
{
    public class AdminService : IAdminService
    {
        public int AddBanRecord(BanRecord data) => new BanRecordDAO().AddBanRecord(data);

        public int ForceUnban(Guid userId, string reason) => new BanRecordDAO().ForceUnban(userId, reason);

        public int SetIsBanned(bool choice, Guid userId) => new AccountDAO().SetIsBanned(choice, userId);
    }
}

