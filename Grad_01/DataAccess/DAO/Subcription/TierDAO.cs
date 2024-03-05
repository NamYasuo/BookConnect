using System;
using BusinessObjects;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO.Subcription
{
	public class TierDAO
	{
		public int AddNewTier(Tier tier)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Tiers.Add(tier);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<Tier> GetAllTierOfUser(Guid creatorId)
		{
			try
			{
				List<Tier> tiers = new List<Tier>();
				using(var context = new AppDbContext())
				{
					tiers = context.Tiers.Where(t => t.CreatorId == creatorId).ToList();
				}
				return tiers;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

