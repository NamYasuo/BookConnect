using System;
using System.Collections.Generic;
using APIs.Repositories.Interfaces;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using DataAccess.DAO;

namespace APIs.Repositories
{
    public class SellRepository : ISellRepository
    {
        public void AddToBookListing(BookListingManageDTOs item) => new SellDAO().AddBookListing(item);

        public void UpdateBookListing(BookListingManageDTOs item) => new SellDAO().UpdateBookListing(item);

        public void RemoveFromBookListing(Guid itemId) => new SellDAO().DeleteBookListing(itemId);

        public List<BookListing> GetBookListingById(Guid Id) => new SellDAO().GetBookListingById(Id);
        public List<BookListing> GetBookListingByName(string listName) => new SellDAO().GetBookListingByName(listName);
        public void AddToInventory(InventoryManageDTOs item) => new SellDAO().AddInventoryItem(item);

        public void UpdateInventoryItem(InventoryManageDTOs item) => new SellDAO().UpdateInventoryItem(item);

        public void RemoveFromInventory(Guid itemId) => new SellDAO().DeleteInventoryItem(itemId);

        public List<Inventory> GetInventoryItemsById(Guid Id) => new SellDAO().GetInventoryItemsById(Id);

    }
}

