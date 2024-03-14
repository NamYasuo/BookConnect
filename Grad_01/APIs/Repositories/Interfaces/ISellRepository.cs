using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using BusinessObjects.DTO;

namespace APIs.Repositories.Interfaces
{
    public interface ISellRepository
    {
        // Methods for managing book listings
        void AddToBookListing(BookListingManageDTOs item);
        void UpdateBookListing(BookListingManageDTOs item);
        void RemoveFromBookListing(Guid itemId);
        List<BookListing> GetBookListingById(Guid Id);
        List<BookListing> GetBookListingByName(string listName);


        // Methods for managing inventory
        void AddToInventory(InventoryManageDTOs item);
        void UpdateInventoryItem(InventoryManageDTOs item);
        void RemoveFromInventory(Guid itemId);
        List<Inventory> GetInventoryItemsById(Guid Id);
    }
}


