using System;
using System.Collections.Generic;
using APIs.Services.Intefaces;
using BusinessObjects.Models;
using DataAccess.DAO;
using BusinessObjects.DTO;
using BusinessObjects;

namespace APIs.Services
{
    public class BookService : IBookService
    {
       

        public List<Book> GetAllBook() => new BookDAO().GetAllBook();
        
        public List<Book> GetBookByCategoryName(string[] cateName)
        {
            try
            {
                // Use the BookDAO to retrieve books based on category
                BookDAO bookDAO = new BookDAO();
                return bookDAO.GetBookByCategoryName(cateName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Book> GetBookByType(string type) => new BookDAO().GetBookByType(type);
        

        public List<Book> GetBookByName(string searchTerm) => new BookDAO().GetBookByName(searchTerm);

        public List<Book> FilterBooks(string searchTerm = null, string[] categoryNames = null, string type = null)
        {
            List<Book> filteredBooks = new List<Book>();

            // Filter by name (optional)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredBooks = GetBookByName(searchTerm);
            }
            else
            {
                filteredBooks = GetAllBook(); // Get all books if no search term
            }

            // Filter by category names (optional)
            if (categoryNames != null && categoryNames.Any())
            {
                filteredBooks = GetBookByCategoryName(categoryNames).Intersect(filteredBooks).ToList();
            }

            // Filter by type (optional)
            if (type != null && (type == "OLD" || type == "NEW"))
            {
                filteredBooks = GetBookByType(type).Intersect(filteredBooks).ToList();
            }

            return filteredBooks;
        }
    }
}
