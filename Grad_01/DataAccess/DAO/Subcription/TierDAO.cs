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

		public int GetTierDurationById(Guid tierId)
		{
			try
			{
				int result = 0;
				using(var context = new AppDbContext())
				{
					Tier? tier = context.Tiers.Where(t => t.TierId == tierId).SingleOrDefault();


                    if (tier != null)
					{
                        result = tier.Duration;
                    }
                }
				return result;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Tier? GetTierById(Guid tierId)
		{
			try
			{
				Tier? tier = null;
				using(var context = new AppDbContext())
				{
					tier = context.Tiers.Where(t => t.TierId == tierId).SingleOrDefault();
				}
				return tier;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool IsCreatorOfTier(Guid userId, Guid tierId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					return context.Tiers.Any(t => t.CreatorId == userId && t.TierId == tierId);
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

