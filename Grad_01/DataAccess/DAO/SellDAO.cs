//using System;
//using System.Collections.Generic;
//using BusinessObjects.Models;
//using DataAccess.Interfaces;

//namespace DataAccess.DAO
//{
//    public class SellDAO : ISellDAO
//    {
//        private readonly AppDbContext _context;

//        public SellDAO(AppDbContext context)
//        {
//            _context = context;
//        }

//        // Book Listing CRUD operations

//        public void AddNewBook(Book book)
//        {
//            _context.Books.Add(book);
//            _conte4xt.SaveChanges();
//        }

//        public void UpdateBook(Book book)
//        {
            
//            _context.Books.Update(book);
//            _context.SaveChanges();
//        }

//        public void DeleteBookById(Guid bookId)
//        {
//            var book = _context.Books.Find(bookId);
//            if (book != null)
//            {
//                _context.Books.Remove(book);
//                _context.SaveChanges();
//            }
//        }

//        public List<Book> GetAllBooks()
//        {
//            return _context.Books.ToList();
//        }

//        // Inventory operations

//        public void AddToInventory(InventoryItem item)
//        {
//            _context.InventoryItems.Add(item);
//            _context.SaveChanges();
//        }

//        public void UpdateInventoryItem(InventoryItem item)
//        {
//            _context.InventoryItems.Update(item);
//            _context.SaveChanges();
//        }

//        public void RemoveFromInventory(Guid itemId)
//        {
//            var item = _context.InventoryItems.Find(itemId);
//            if (item != null)
//            {
//                _context.InventoryItems.Remove(item);
//                _context.SaveChanges();
//            }
//        }

//        public List<InventoryItem> GetInventoryItemsBySellerId(Guid sellerId)
//        {
//            return _context.InventoryItems.Where(item => item.SellerId == sellerId).ToList();
//        }

//        // Additional methods for communication and order processing
//    }
//}
