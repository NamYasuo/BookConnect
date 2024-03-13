using System;
using System.Collections.Generic;
using APIs.Repositories.Interfaces;
using BusinessObjects.Models;
using DataAccess.DAO;

namespace APIs.Repositories
{
    public class SellRepository : ISellRepository
    {
        public void AddToBookListing(BookListing item) => new SellDAO().AddBookListing(item);

        public void UpdateBookListing(BookListing item) => new SellDAO().UpdateBookListing(item);

        public void RemoveFromBookListing(Guid itemId) => new SellDAO().DeleteBookListing(itemId);

        public List<BookListing> GetBookListingBySellerId(Guid sellerId) => new SellDAO().GetBookListingBySellerId(sellerId);
        public List<BookListing> GetBookListingByName(string listName) => new SellDAO().GetBookListingByName(listName);
        public void AddToInventory(Inventory item) => new SellDAO().AddInventoryItem(item);

        public void UpdateInventoryItem(Inventory item) => new SellDAO().UpdateInventoryItem(item);

        public void RemoveFromInventory(Guid itemId) => new SellDAO().DeleteInventoryItem(itemId);

        public List<Inventory> GetInventoryItemsBySellerId(Guid sellerId) => new SellDAO().GetInventoryItemsBySellerId(sellerId);

    }
}

