using System;
using System.Collections.Generic;
using APIs.Services.Interfaces;
using BusinessObjects.Models;
using DataAccess.DAO;
using BusinessObjects.DTO;
using BusinessObjects;

namespace APIs.Services
{
    public class BookService : IBookService
    {

        public List<Book> GetAllBook() => new BookDAO().GetAllBook();


        public Book GetBookById(Guid bookId) => new BookDAO().GetBookById(bookId);


        public Book GetBookByName(string name) => new BookDAO().GetBookByName(name);


        public void AddNewBook(Book book) => new BookDAO().AddNewBook(book);


        public void UpdateBook(Book book) => new BookDAO().UpdateBook(book);


        public void DeleteBook(Guid bookId) => new BookDAO().DeleteBook(bookId);

        public BookDetailsDTO GetBookDetailsById(Guid bookId) => new BookDAO().GetBookDetailsById(bookId);


        public List<Book> GetBookListById(List<Guid> bookIds) => new BookDAO().GetBookListById(bookIds);

        public List<SEODTO> ListSEO(string searchTerm)
        {
            List<SEODTO> result = new List<SEODTO>();
            try
            {
                using (var context = new AppDbContext()) // Assuming you use Entity Framework
                {
                    var searchResult = context.Books.Where(b => b.Name.Contains(searchTerm)).ToList();

                    foreach (Book book in searchResult)
                    {
                        result.Add(new SEODTO
                        {
                            BookId = book.ProductId,
                            Title = book.Name,
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
