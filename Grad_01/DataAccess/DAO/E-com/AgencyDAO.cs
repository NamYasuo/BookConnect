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
    }
}

