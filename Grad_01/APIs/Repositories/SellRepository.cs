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

//        public void DeleteBookListing(Guid bookId) => new BookDAO().DeleteBook(bookId);

        public List<BookListing> GetBookListingById(Guid Id) => new SellDAO().GetBookListingById(Id);
        public List<BookListing> GetBookListingByName(string listName) => new SellDAO().GetBookListingByName(listName);
        public void AddToInventory(InventoryManageDTOs item) => new SellDAO().AddInventoryItem(item);

        public void UpdateInventoryItem(InventoryManageDTOs item) => new SellDAO().UpdateInventoryItem(item);

//        public void RemoveFromInventory(Guid itemId) => new InventoryDAO().DeleteInventoryItem(itemId);

        public List<Inventory> GetInventoryItemsById(Guid Id) => new SellDAO().GetInventoryItemsById(Id);
        public void AddToAds(AdsManageDTOs item) => new SellDAO().AddAds(item);

        public void UpdateAds(AdsManageDTOs item) => new SellDAO().UpdateAds(item);

        public void RemoveFromAds(Guid itemId) => new SellDAO().DeleteAds(itemId);

        public List<Ads> GetAdsById(Guid Id) => new SellDAO().GetAdsById(Id);
        public void SendMessage(MessageDTOs message) => new MessageDAO().SendMessage(message);
 
        public List<MessageDTOs> GetMessages(Guid senderId, Guid receiverId) => new MessageDAO().GetMessages(senderId, receiverId);
    }
}

