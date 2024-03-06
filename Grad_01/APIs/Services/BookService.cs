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
        

        public List<Book> Get(string searchTerm) => new BookDAO().GetBookByName(searchTerm);

        public List<Book> GetBookByName(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
