using System;
using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.DTO;
using DataAccess;
using BusinessObjects.Models;
using System.Linq.Expressions;


namespace DataAccess.DAO
{
    public class SellDAO
    {
        // Book Listing operations

        public void AddBookListing(BookListingManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var entity = new BookListing
                    {
                        ListingId = item.ListingId,
                        BookId = item.BookId,
                        ListName = item.ListName,
                        ListDescription = item.ListDescription,
                        DateAdded = DateTime.Now,
                        Quantity = item.Quantity
                    };
                    context.BookListings.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateBookListing(BookListingManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingItem = context.BookListings.FirstOrDefault(i => i.ListingId == item.ListingId);
                    if (existingItem != null)
                    {
                        existingItem.ListingId = item.ListingId;
                        existingItem.BookId = item.BookId;
                        existingItem.ListName = item.ListName;
                        existingItem.ListDescription = item.ListDescription;
                        existingItem.DateAdded = DateTime.Now;
                        existingItem.Quantity = item.Quantity;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteBookListing(Guid itemId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var item = context.BookListings.FirstOrDefault(i => i.ListingId == itemId);
                    if (item != null)
                    {
                        context.BookListings.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookListing> GetBookListingById(Guid Id)
        {
            try { 
            using (var context = new AppDbContext())
            {
                return context.BookListings.Where(i => i.ListingId == Id).ToList();
            }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<BookListing> GetBookListingByName(string listName)
        {
            try { 
            using (var context = new AppDbContext())
            {
                return context.BookListings
                       .Where(bl => bl.ListName.Contains(listName))
                       .ToList();
            }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Inventory operations

        public void AddInventoryItem(InventoryManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var entity = new Inventory
                    {
                        InventoryId = item.InventoryId,
                        BookId = item.BookId,
                        Price = item.Price,
                        DateAdded = DateTime.Now,
                        Quantity = item.Quantity
                    };
                    context.Inventories.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateInventoryItem(InventoryManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingItem = context.Inventories.FirstOrDefault(i => i.InventoryId == item.InventoryId);
                    if (existingItem != null)
                    {
                        existingItem.BookId = item.BookId;
                        existingItem.Price = item.Price;
                        existingItem.DateAdded = DateTime.Now;
                        existingItem.Quantity = item.Quantity;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteInventoryItem(Guid itemId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var item = context.Inventories.FirstOrDefault(i => i.InventoryId == itemId);
                    if (item != null)
                    {
                        context.Inventories.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Inventory> GetInventoryItemsById(Guid Id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return context.Inventories.Where(i => i.InventoryId == Id).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Ads operations

        public void AddAds(AdsManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var entity = new Ads
                    {
                        AdsId = item.AdsId,
                        Donors = item.Donors,
                        Description = item.Description,
                        DateAdded = item.DateAdded
                    };
                    context.Ad.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateAds(AdsManageDTOs item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingItem = context.Ad.FirstOrDefault(i => i.AdsId == item.AdsId);
                    if (existingItem != null)
                    {
                        existingItem.Description = item.Description;
                        existingItem.Donors = item.Donors;
                        existingItem.DateAdded = DateTime.Now;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteAds(Guid itemId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var item = context.Ad.FirstOrDefault(i => i.AdsId == itemId);
                    if (item != null)
                    {
                        context.Ad.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Ads> GetAdsById(Guid Id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return context.Ad.Where(i => i.AdsId == Id).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Additional methods for communication and order processing
    }
}
