using System;
using BusinessObjects.Models.Creative;

namespace APIs.Services.Interfaces
{
	public interface ISubscriptionService
	{
		public int AddNewTier(Tier tier);
		public List<Tier> GetTierListByUserId(Guid userId);
	}
}

