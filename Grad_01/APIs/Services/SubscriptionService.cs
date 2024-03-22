using System;
using APIs.Services.Interfaces;
using BusinessObjects.Models.Creative;
using DataAccess.DAO.Subcription;

namespace APIs.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public int AddNewSub(Subscription newSub) => new SubDAO().AddNewSub(newSub);

        public int AddNewTier(Tier tier) => new TierDAO().AddNewTier(tier);

        public Tier? GetTierById(Guid tierId) => new TierDAO().GetTierById(tierId);
       
        public int GetTierDurationById(Guid tierId) => new TierDAO().GetTierDurationById(tierId);

        public List<Tier> GetTierListByUserId(Guid userId) => new TierDAO().GetAllTierOfUser(userId);

        public bool IsCreatorOfTier(Guid userId, Guid tierId) => new TierDAO().IsCreatorOfTier(userId, tierId);
    }
}

