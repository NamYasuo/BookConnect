using System;
using System.Collections.Generic;
using BusinessObjects.Models;

namespace APIs.Repositories.Interfaces
{
    public interface ISellRepository
    {
        // Methods for managing book listings
        void AddToBookListing(BookListing item);
        void UpdateBookListing(BookListing item);
        void RemoveFromBookListing(Guid itemId);
        List<BookListing> GetBookListingBySellerId(Guid sellerId);
        List<BookListing> GetBookListingByName(string listName);


        // Methods for managing inventory
        void AddToInventory(Inventory item);
        void UpdateInventoryItem(Inventory item);
        void RemoveFromInventory(Guid itemId);
        List<Inventory> GetInventoryItemsBySellerId(Guid sellerId);
    }
}


