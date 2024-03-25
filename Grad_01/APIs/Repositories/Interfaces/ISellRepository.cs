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

        // Methods for managing ads
        void AddToAds(AdsManageDTOs item);
        void UpdateAds(AdsManageDTOs item);
        void RemoveFromAds(Guid itemId);
        List<Ads> GetAdsById(Guid Id);

        //Method for communicate

        public void SendMessage(MessageDTOs message);
        public List<MessageDTOs> GetMessages(Guid senderId, Guid receiverId);
    }
}


