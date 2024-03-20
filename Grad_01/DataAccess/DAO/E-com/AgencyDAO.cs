using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;

namespace DataAccess.DAO.Ecom
{
	public class AgencyDAO
	{
		public NameAndIdDTO GetNameAndId(Guid bookId)
		{
			Guid agencyId = Guid.Empty;

            using (var context = new AppDbContext())
			{
				Inventory? invent = context.Inventories.Where(i => i.BookId == bookId).FirstOrDefault();

                if (invent != null && invent.AgencyId != null)
				{
                   agencyId = invent.AgencyId;
                }

                NameAndIdDTO dto = new NameAndIdDTO()
				{
					AgencyId = agencyId,
					AgencyName = context.Agencies.Where(i => i.AgencyId == agencyId).FirstOrDefault()?.AgencyName
				};
				return dto;
			}
		}

		public int GetProductStock(Guid productId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Inventory? record = context.Inventories.Where(i => i.BookId == productId).SingleOrDefault();

					if (record == null) return 0;
					else
					{
                    return record.Quantity;
                    }
                }

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}

