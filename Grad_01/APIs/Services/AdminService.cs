using System;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom.Base;
using DataAccess.DAO;
using DataAccess.DAO.Ecom;

namespace APIs.Services
{
    public class AdminService : IAdminService
    {
        public int AddBanRecord(BanRecord data) => new BanRecordDAO().AddBanRecord(data);

        public int ForceUnban(Guid userId, string reason) => new BanRecordDAO().ForceUnban(userId, reason);

        public PagedList<Agency> GetAllAgency(PagingParams param)
        {
            return PagedList<Agency>.ToPagedList(new AgencyDAO().GetAllAgency()?.OrderBy(a => a.AgencyName).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public int SetIsBanned(bool choice, Guid userId) => new AccountDAO().SetIsBanned(choice, userId);
    }
}

