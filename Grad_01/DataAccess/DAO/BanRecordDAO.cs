using System;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Base;

namespace DataAccess.DAO
{
	public class BanRecordDAO
	{
		public int AddBanRecord(BanRecord data)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.BanRecords.Add(data);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public int ForceUnban(Guid userId, string reason)
		{
			try
			{
				using(var context = new AppDbContext())
				{
                     DateTime? latestUnbannedDate = context.BanRecords
                    .Where(r => r.TargetUserId == userId)
                    .OrderByDescending(r => r.UnbannedDate)
                    .Select(r => r.UnbannedDate)
                    .FirstOrDefault();
                    BanRecord? record = context.BanRecords.Where(r => r.TargetUserId == userId && r.UnbannedDate == latestUnbannedDate).SingleOrDefault();
					if(record != null)
					{
						record.UnbannedDate = DateTime.Now;
						record.UnBanReason = reason;
					}
                    AppUser? user = context.AppUsers.Where(u => u.UserId == userId).SingleOrDefault();
                    if (user != null)
                    {
                        user.IsBanned = false;
                    }

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

