using System;
using System.Collections.Generic;
using BusinessObjects;
using DataAccess;
using BusinessObjects.Models;


namespace DataAccess.DAO
{
    public class SellDAO
    {
        // Book Listing CRUD operations

        public void AddBookListing(BookListing item)
        {
            using (var context = new AppDbContext())
            {
                context.BookListings.Add(item);
                context.SaveChanges();
            }
        }

        public void UpdateBookListing(BookListing item)
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
                    existingItem.DateAdded = item.DateAdded;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteBookListing(Guid itemId)
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

        public List<BookListing> GetBookListingBySellerId(Guid sellerId)
        {
            using (var context = new AppDbContext())
            {
                return context.BookListings.Where(i => i.AgencyId == sellerId).ToList();
            }
        }

        public List<BookListing> GetBookListingByName(string listName)
        {
            using (var context = new AppDbContext())
            {
                return context.BookListings
                       .Where(bl => bl.ListName.Contains(listName))
                       .ToList();
            }
        }

        // Inventory operations

        public void AddInventoryItem(Inventory item)
        {
            using (var context = new AppDbContext())
            {
                context.Inventories.Add(item);
                context.SaveChanges();
            }
        }

        public void UpdateInventoryItem(Inventory item)
        {
            using (var context = new AppDbContext())
            {
                var existingItem = context.Inventories.FirstOrDefault(i => i.InventoryId == item.InventoryId);
                if (existingItem != null)
                {
                    existingItem.InventoryId = item.InventoryId;
                    existingItem.BookId = item.BookId;
                    existingItem.Quantity = item.Quantity;
                    existingItem.Price = item.Price;
                    existingItem.DateAdded = item.DateAdded;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteInventoryItem(Guid itemId)
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

        public List<Inventory> GetInventoryItemsBySellerId(Guid sellerId)
        {
            using (var context = new AppDbContext())
            {
                return context.Inventories.Where(i => i.AgencyId == sellerId).ToList();
            }
        }

        // Additional methods for communication and order processing
    }
}
