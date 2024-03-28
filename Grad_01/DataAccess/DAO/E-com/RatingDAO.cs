using System;
using BusinessObjects;
using BusinessObjects.Models.Ecom.Rating;
using BusinessObjects.Models.Trading;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO.Ecom
{
	public class RatingDAO
	{
		public double? GetOverallRatingById(Guid ratingId)
        {
            try
			{
                double? result = null;

                using (var context = new AppDbContext())
				{
                    Rating? rate = context.Ratings.Where(r => r.RatingId == ratingId).FirstOrDefault();
                    if(rate != null)
					{
                        result = rate?.OverallRating;
                    }
                }
                return result;
            }
            catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public int RateAndComment(RatingRecord ratingRecord)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.RatingRecords.Add(ratingRecord);
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

