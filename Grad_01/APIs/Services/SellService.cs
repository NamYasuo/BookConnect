using System;
using System.Collections.Generic;
using APIs.DTO;
using BusinessObjects.Models;
using APIs.Services.Interfaces;
using APIs.Repositories.Interfaces;
using System.ComponentModel.Design;

namespace APIs.Services
{
    public class SellService
    {
        private readonly IOrderService _orderService;
        private readonly ISellService _sellRepository;


        // Book Listing Services
        public void AddToBookListing(BookListing item) => _sellRepository.AddToBookListing(item);

        public void UpdateBookListing(BookListing item) => _sellRepository.UpdateBookListing(item);

        public void RemoveFromBookListing(Guid itemId) => _sellRepository.RemoveFromBookListing(itemId);

        public List<BookListing> GetBookListingBySellerId(Guid sellerId) => _sellRepository.GetBookListingBySellerId(sellerId);
        public List<BookListing> GetBookListingByName(string listName) => _sellRepository.GetBookListingByName(listName);
        // Inventory Services
        public void AddToInventory(Inventory item) => _sellRepository.AddToInventory(item);

        public void UpdateInventoryItem(Inventory item) => _sellRepository.UpdateInventoryItem(item);

        public void RemoveFromInventory(Guid itemId) => _sellRepository.RemoveFromInventory(itemId);

        public List<Inventory> GetInventoryItemsBySellerId(Guid sellerId) => _sellRepository.GetInventoryItemsBySellerId(sellerId);

        //// Communication Services
        //public void SendMessageToBuyer(Guid sellerId, Guid buyerId, string message) => _messageService.SendMessage(sellerId, buyerId, message);

        // Order Processing Services
        //public string ProcessOrder(Guid orderId) => _orderService.ProcessOrder(orderId);

        //public string UpdateOrderStatus(Guid orderId, string newStatus) => _orderService.UpdateOrderStatus(orderId, newStatus);

        //public string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo) => _orderService.UpdateShippingInformation(orderId, shippingInfo);
    }
}

