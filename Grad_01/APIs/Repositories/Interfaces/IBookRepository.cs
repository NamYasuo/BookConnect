using System;
using BusinessObjects.Models;
using DataAccess.DAO;

namespace APIs.Repositories.Interfaces
{
	public interface IBookRepository
	{
        public void DeleteBookById(Guid id);

        public void AddNewBook(Book book);

        public List<Book> GetAllBook();

        public Book GetBookById(Guid id);

        public void UpdateBook(Book book);

        public List<Book> GetBookListById(List<Guid> bookIds);
    }
}

