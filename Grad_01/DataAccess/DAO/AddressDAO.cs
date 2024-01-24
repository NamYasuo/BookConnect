using System;
using BusinessObjects;
using BusinessObjects.Models;

namespace DataAccess.DAO
{
	public class AddressDAO
	{
		//Get all address of a user
		public List<Address> GetAllUserAddress(Guid userId)
		{
			List<Address> result = new List<Address>();
			try
			{
				using (var context = new AppDbContext())
				{
					result = context.Addresses.Where(u => u.AddressId == userId).ToList();
                    return result;
                }

            }
            catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Address GetUserDefaultAddress(Guid userId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					//Handle this null warning
					return context.Addresses.Where(a => a.UserId == userId && a.Default == true).FirstOrDefault();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

