using System;
using System.Collections.Generic;
using BusinessObjects.DTO;
using BusinessObjects.Models;


namespace APIs.Services.Interfaces
{
    public interface ISellService
    {
        // Book Listing Services
        void AddToBookListing(BookListingManageDTOs item);
        void UpdateBookListing(BookListingManageDTOs item);
        void RemoveFromBookListing(Guid itemId);
        List<BookListing> GetBookListingById(Guid Id);
        List<BookListing> GetBookListingByName(string listName);

        // Inventory Services
        void AddToInventory(InventoryManageDTOs item);
        void UpdateInventoryItem(InventoryManageDTOs item);
        void RemoveFromInventory(Guid itemId);
        List<Inventory> GetInventoryItemsById(Guid Id);

        //// Communication Services
        //void SendMessageToBuyer(Guid sellerId, Guid buyerId, string message);

        //// Order Processing Services
        //string ProcessOrder(Guid orderId);
        //string UpdateOrderStatus(Guid orderId, string newStatus);
        ////string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo);
    }
}

