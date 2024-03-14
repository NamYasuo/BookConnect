using System;
using System.Collections.Generic;
using APIs.DTO;
using BusinessObjects.Models;

namespace APIs.Services.Interfaces
{
    public interface ISellService
    {
        // Book Listing Services
        void AddToBookListing(BookListing item);
        void UpdateBookListing(BookListing item);
        void RemoveFromBookListing(Guid itemId);
        List<BookListing> GetBookListingBySellerId(Guid sellerId);
        List<BookListing> GetBookListingByName(string listName);

        // Inventory Services
        void AddToInventory(Inventory item);
        void UpdateInventoryItem(Inventory item);
        void RemoveFromInventory(Guid itemId);
        List<Inventory> GetInventoryItemsBySellerId(Guid sellerId);

        //// Communication Services
        //void SendMessageToBuyer(Guid sellerId, Guid buyerId, string message);

        //// Order Processing Services
        //string ProcessOrder(Guid orderId);
        //string UpdateOrderStatus(Guid orderId, string newStatus);
        ////string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo);
    }
}

