//using System;
//using System.Collections.Generic;
//using APIs.Repositories.Interfaces;
//using BusinessObjects.Models;
//using DataAccess.DAO;

//namespace APIs.Repositories
//{
//    public class SellRepository : ISellRepository
//    {
//        public void CreateBookListing(Book book) => new BookDAO().AddNewBook(book);

//        public void UpdateBookListing(Book book) => new BookDAO().UpdateBook(book);

//        public void DeleteBookListing(Guid bookId) => new BookDAO().DeleteBook(bookId);

//        public List<Book> GetAllBookListings() => new BookDAO().GetAllBook();

//        public Book GetBookListingById(Guid bookId) => new BookDAO().GetBookById(bookId);

//        public void AddToInventory(InventoryItem item) => new InventoryDAO().AddInventoryItem(item);

//        public void UpdateInventoryItem(InventoryItem item) => new InventoryDAO().UpdateInventoryItem(item);

//        public void RemoveFromInventory(Guid itemId) => new InventoryDAO().DeleteInventoryItem(itemId);

//        public List<InventoryItem> GetInventoryItemsBySellerId(Guid sellerId) => new InventoryDAO().GetInventoryItemsBySellerId(sellerId);
//    }
//}

