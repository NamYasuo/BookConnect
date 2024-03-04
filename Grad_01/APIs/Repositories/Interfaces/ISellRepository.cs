using System;
using System.Collections.Generic;
using BusinessObjects.Models;

namespace APIs.Repositories.Interfaces
{
    public interface ISellRepository
    {
        // Methods for managing book listings
        void CreateBookListing(Book book);
        void UpdateBookListing(Book book);
        void DeleteBookListing(Guid bookId);
        List<Book> GetAllBookListings();
        Book GetBookListingById(Guid bookId);

        // Methods for managing inventory
        void AddToInventory(Inventory item);
        void UpdateInventoryItem(Inventory item);
        void RemoveFromInventory(Guid itemId);
        List<Inventory> GetInventoryItemsBySellerId(Guid sellerId);
    }
}
