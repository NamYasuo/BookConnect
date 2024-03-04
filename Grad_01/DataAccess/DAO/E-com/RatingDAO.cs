using System;
using BusinessObjects;
using BusinessObjects.Models.Ecom.Rating;
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
	}
}

