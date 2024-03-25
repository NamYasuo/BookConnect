//using System;
//using System.Collections.Generic;
//using APIs.DTO;
//using BusinessObjects.Models;

//namespace APIs.Services.Interfaces
//{
//    public interface ISellService
//    {
//        // Book Listing Services
//        void CreateBookListing(Book book);
//        void UpdateBookListing(Book book);
//        void DeleteBookListing(Guid bookId);
//        List<Book> GetAllBookListings();

//        // Inventory Services
//        void AddToInventory(InventoryItem item);
//        void UpdateInventoryItem(InventoryItem item);
//        void RemoveFromInventory(Guid itemId);
//        List<InventoryItem> GetInventoryItemsBySellerId(Guid sellerId);

//        // Communication Services
//        void SendMessageToBuyer(Guid sellerId, Guid buyerId, string message);

//        // Order Processing Services
//        string ProcessOrder(Guid orderId);
//        string UpdateOrderStatus(Guid orderId, string newStatus);
//        string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo);
//    }
//}


