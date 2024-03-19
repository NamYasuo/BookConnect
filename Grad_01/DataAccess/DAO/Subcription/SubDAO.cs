using System;
using BusinessObjects;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO.Subcription
{
	public class SubDAO
	{
		public int AddNewSub(Subscription newSub)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Subscriptions.Add(newSub);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

