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

        public void AddNewBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Guid bookId);
        public List<Book> GetBookListById(List<Guid> bookIds);


        List<SEODTO> ListSEO(string searchTerm);

        //-----------------------------------Book category------------------------------------------------//
        int AddBookToCategory(Guid bookId, List<Guid> cateId);
        bool IsBookAlreadyInCate(Guid bookId, Guid cateId);
        int RemoveBookFromCate(Guid bookId, Guid cateId);
        List<Category> GetAllCategoryOfBook(Guid bookId);
        List<Book> GetBookByName(string searchTerm);
        public List<Book> GetBookByCategoryName(string[] cateName);
        public List<Book> GetBookByType(string type);
        public List<Book> FilterBooks(string searchTerm = null, string[] categoryNames = null, string type = null);
    }
}

