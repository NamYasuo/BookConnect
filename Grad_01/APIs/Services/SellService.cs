//using System;
//using System.Collections.Generic;
//using APIs.DTO;
//using BusinessObjects.Models;
//using APIs.Services.Interfaces;
//using APIs.Repositories.Interfaces;
//using System.ComponentModel.Design;

//namespace APIs.Services
//{
//    public class SellService : ISellService
//    {
//        private readonly IBookRepository _bookRepository;
//        private readonly IInventoryRepository _inventoryRepository;
//        private readonly IMessageService _messageService;
//        private readonly IOrderService _orderService;

//        public SellService(IBookRepository bookRepository, IInventoryRepository inventoryRepository, IMessageService messageService, IOrderService orderService)
//        {
//            _bookRepository = bookRepository;
//            _inventoryRepository = inventoryRepository;
//            _messageService = messageService;
//            _orderService = orderService;
//        }

//        // Book Listing Services
//        public void CreateBookListing(Book book) => _bookRepository.AddNewBook(book);

//        public void UpdateBookListing(Book book) => _bookRepository.UpdateBook(book);

//        public void DeleteBookListing(Guid bookId) => _bookRepository.DeleteBookById(bookId);

//        public List<Book> GetAllBookListings() => _bookRepository.GetAllBook();

//        // Inventory Services
//        public void AddToInventory(InventoryItem item) => _inventoryRepository.AddToInventory(item);

//        public void UpdateInventoryItem(InventoryItem item) => _inventoryRepository.UpdateInventoryItem(item);

//        public void RemoveFromInventory(Guid itemId) => _inventoryRepository.RemoveFromInventory(itemId);

//        public List<InventoryItem> GetInventoryItemsBySellerId(Guid sellerId) => _inventoryRepository.GetInventoryItemsBySellerId(sellerId);

//        // Communication Services
//        public void SendMessageToBuyer(Guid sellerId, Guid buyerId, string message) => _messageService.SendMessage(sellerId, buyerId, message);

//        // Order Processing Services
//        public string ProcessOrder(Guid orderId) => _orderService.ProcessOrder(orderId);

//        public string UpdateOrderStatus(Guid orderId, string newStatus) => _orderService.UpdateOrderStatus(orderId, newStatus);

//        public string UpdateShippingInformation(Guid orderId, ShippingInfoDTO shippingInfo) => _orderService.UpdateShippingInformation(orderId, shippingInfo);
//    }
//}


