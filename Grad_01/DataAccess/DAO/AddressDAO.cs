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
					result = context.Addresses.Where(u => u.UserId == userId).ToList();

                    return result;
                }

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public Address GetUserDefaultAddress(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    //Handle this null warning
                    return context.Addresses.Where(a => a.UserId == userId && a.Default == true).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int AddNewAddress(Address address)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Addresses.Add(address);
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateAddress(Guid userId, Guid? addressId, string cityProvince, string district, string subDistrict, string rendezvous, bool isDefault)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    if (addressId.HasValue)
                    {
                        // Update existing address
                        var existingAddress = context.Addresses.FirstOrDefault(a => a.UserId == userId && a.AddressId == addressId);

                        if (existingAddress == null)
                        {
                            throw new Exception("Address not found.");
                        }

                        existingAddress.City_Province = cityProvince;
                        existingAddress.District = district;
                        existingAddress.SubDistrict = subDistrict;
                        existingAddress.Rendezvous = rendezvous;
                        existingAddress.Default = isDefault;
                    }
                    else
                    {
                        // Create new address
                        var newAddress = new Address
                        {
                            AddressId = Guid.NewGuid(),
                            UserId = userId,
                            City_Province = cityProvince,
                            District = district,
                            SubDistrict = subDistrict,
                            Rendezvous = rendezvous,
                            Default = isDefault
                        };

                        context.Addresses.Add(newAddress);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error updating address: " + e.Message);
            }
        }



    }
}



