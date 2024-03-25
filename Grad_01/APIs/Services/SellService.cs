using System;
using System.Collections.Generic;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using APIs.Services.Interfaces;
using APIs.Repositories.Interfaces;
using System.ComponentModel.Design;
using DataAccess.DAO;

//namespace APIs.Services
//{
    //public class SellService
    //{
    //    private readonly IOrderService _orderService;
    //    private readonly ISellService _sellRepository;

    //    // Book Listing Services
    //    public void AddToBookListing(BookListingManageDTOs item) => _sellRepository.AddToBookListing(item);

    //    public void UpdateBookListing(BookListingManageDTOs item) => _sellRepository.UpdateBookListing(item);

    //    public void RemoveFromBookListing(Guid itemId) => _sellRepository.RemoveFromBookListing(itemId);

    //    public List<BookListing> GetBookListingById(Guid Id) => _sellRepository.GetBookListingById(Id);
    //    public List<BookListing> GetBookListingByName(string listName) => _sellRepository.GetBookListingByName(listName);
    //    // Inventory Services
    //    public void AddToInventory(InventoryManageDTOs item) => _sellRepository.AddToInventory(item);

    //    public void UpdateInventoryItem(InventoryManageDTOs item) => _sellRepository.UpdateInventoryItem(item);

    //    //        public void RemoveFromInventory(Guid itemId) => _inventoryRepository.RemoveFromInventory(itemId);

    //    public List<Inventory> GetInventoryItemsById(Guid Id) => _sellRepository.GetInventoryItemsById(Id);
    //    // Ads Services
    //    public void AddToAds(AdsManageDTOs item) => _sellRepository.AddToAds(item);

    //    public void UpdateAds(AdsManageDTOs item) => _sellRepository.UpdateAds(item);

    //    public void RemoveFromAds(Guid itemId) => _sellRepository.RemoveFromAds(itemId);

    //    public List<Inventory> GetAdsById(Guid Id) => _sellRepository.GetAdsById(Id);
    //    //// Communication Services
    //    public void SendMessage(MessageDTOs message) => _sellRepository.SendMessage(message);

    //    public List<MessageDTOs> GetMessages(Guid senderId, Guid receiverId) => _sellRepository.GetMessages(senderId, receiverId);
//    }
//}
        // Order Processing Services
        //public string ProcessOrder(Guid orderId) => _orderService.ProcessOrder(orderId);

//        public string UpdateOrderStatus(Guid orderId, string newStatus) => _orderService.UpdateOrderStatus(orderId, newStatus);

//        public string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo) => _orderService.UpdateShippingInformation(orderId, shippingInfo);
//    }
//}


