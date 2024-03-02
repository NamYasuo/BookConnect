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
        public Book GetBookById(Guid bookId);
        public Book GetBookByName(string name);
        public void AddNewBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Guid bookId);
        public List<Book> GetBookListById(List<Guid> bookIds);


        List<SEODTO> ListSEO(string searchTerm);
    }
}

