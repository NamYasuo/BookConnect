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
        public void UpdateAddress(Address updatedAddress)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingAddress = context.Addresses.FirstOrDefault(a => a.AddressId == updatedAddress.AddressId);
                    if (existingAddress != null)
                    {
                        // Update properties of the existing address
                        existingAddress.City_Province = updatedAddress.City_Province;
                        existingAddress.District = updatedAddress.District;
                        existingAddress.SubDistrict = updatedAddress.SubDistrict;
                        existingAddress.Rendezvous = updatedAddress.Rendezvous;
                        existingAddress.Default = updatedAddress.Default;

                        // Save changes to the database
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Address not found.");
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

