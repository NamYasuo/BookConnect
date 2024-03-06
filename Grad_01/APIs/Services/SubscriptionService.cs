using System;
using APIs.Services.Interfaces;
using BusinessObjects.Models.Creative;
using DataAccess.DAO.Subcription;

namespace APIs.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public int AddNewTier(Tier tier) => new TierDAO().AddNewTier(tier);

        public List<Tier> GetTierListByUserId(Guid userId) => new TierDAO().GetAllTierOfUser(userId);
       
    }
}

