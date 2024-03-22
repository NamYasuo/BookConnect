using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

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

		public int AddNewAgency(Agency agency)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Agencies.Add(agency);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Agency GetAgencyById(Guid agencyId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Agency? result = context.Agencies.Where(a => a.AgencyId == agencyId).SingleOrDefault();
					if (result == null) { return new Agency(); }
					else return result;
                }
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<Agency> GetAgencyByOwnerId(Guid ownerId)
		{
			try
			{
				using (var context = new AppDbContext())
				{
					return context.Agencies.Where(a => a.OwnerId == ownerId).ToList();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public int UpadateAgency(Agency updatedData)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Agencies.Update(updatedData);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Address? GetCurrentAddress(Guid agencyId)
		{
			try
			{
				using(var context = new AppDbContext())
                {
                    Address? address = (from ad in context.Addresses
                                       join ag in context.Agencies on ad.AddressId equals ag.PostAddressId
                                       select ad).SingleOrDefault();
                    return address;
                }
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public string? GetCurrentLogoUrl(Guid agencyId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Agency? agency = context.Agencies.Where(a => a.AgencyId == agencyId).SingleOrDefault();
					string? result = (agency != null) ? agency.LogoUrl : null;
					return result;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<Agency> GetAllAgency()
		{
			try
			{
				using(var context = new AppDbContext())
				{
					return context.Agencies.ToList();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}


	}
}
