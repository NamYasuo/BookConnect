using System;
using BusinessObjects.Models.Creative;

namespace APIs.Services.Interfaces
{
	public interface ISubscriptionService
	{
		public int AddNewTier(Tier tier);
		public List<Tier> GetTierListByUserId(Guid userId);
		public int AddNewSub(Subscription newSub);
		public int GetTierDurationById(Guid tierId);
		public Tier? GetTierById(Guid tierId);
		public bool IsCreatorOfTier(Guid userId, Guid tierId);
    }
}

