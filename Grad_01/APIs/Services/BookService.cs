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
        /*    private readonly BookDAO _bookDAO;*/
        /*
                public BookService(BookDAO bookDAO)
                {
                    _bookDAO = bookDAO;
                }

                public List<Book> GetAllBook()
                {
                    return _bookDAO.GetAllBook();
                }

                public Book GetBookById(Guid bookId)
                {
                    return _bookDAO.GetBookById(bookId);
                }

                public Book GetBookByName(string name)
                {
                    return _bookDAO.GetBookByName(name);
                }

                public void AddNewBook(Book book)
                {
                    _bookDAO.AddNewBook(book);
                }

                public void UpdateBook(Book book)
                {
                    _bookDAO.UpdateBook(book);
                }

                public void DeleteBook(Guid bookId)
                {
                    _bookDAO.DeleteBook(bookId);
                }

                public List<Book> GetBookListById(List<Guid> bookIds)
                {
                    return _bookDAO.GetBookListById(bookIds);
                }*/

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
