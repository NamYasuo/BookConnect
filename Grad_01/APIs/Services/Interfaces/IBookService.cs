using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Identity;

namespace APIs.Services.Interfaces
{
    public interface IBookService
    {
        public List<Book> GetAllBook();
        public BookDetailsDTO GetBookDetailsById(Guid bookId);
        public Book GetBookById(Guid bookId);
        public Book GetBookByName(string name);
        public void AddNewBook(Book item);
        public void UpdateBook(Book item);
        public void DeleteBook(Guid bookId);
        public List<Book> GetBookListById(List<Guid> bookIds);


        List<SEODTO> ListSEO(string searchTerm);
        void UpdateBook(BookDetailsDTO item);
        void AddNewBook(BookDetailsDTO item);
    }
}

