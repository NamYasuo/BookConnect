using System;
using APIs.Services.Interfaces;
using DataAccess.DAO;

namespace APIs.Services
{
    public class AdminService : IAdminService
    {
        public int SetIsBanned(bool choice, Guid userId) => new AccountDAO().SetIsBanned(choice, userId);
    }
}

