using System;
using APIs.Repositories.Interfaces;
using BusinessObjects.Models;
using DataAccess.DAO;

namespace APIs.Repositories
{
	public class BookRepository: IBookRepository
	{
        public void DeleteBookById(Guid id) => new BookDAO().DeleteBook(id);

        public void AddNewBook(Book book) => new BookDAO().AddNewBook(book);

        public List<Book> GetAllBook() => new BookDAO().GetAllBook();

        public Book GetBookById(Guid id) => new BookDAO().GetBookById(id);

        public void UpdateBook(Book book) => new BookDAO().UpdateBook(book);

        public List<Book> GetBookListById(List<Guid> bookIds) => new BookDAO().GetBookListById(bookIds);
       
    }
}

