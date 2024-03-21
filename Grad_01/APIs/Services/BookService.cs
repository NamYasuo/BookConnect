﻿using System;
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
        void IBookService.UpdateBook(BookDetailsDTO item)
        {
            throw new NotImplementedException();
        }

        void IBookService.AddNewBook(BookDetailsDTO item)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBook() => new BookDAO().GetAllBook();


        public Book GetBookById(Guid bookId) => new BookDAO().GetBookById(bookId);


        public Book GetBookByName(string name) => new BookDAO().GetBookByName(name);


        public void AddNewBook(Book item) => new BookDAO().AddNewBook(item);


        public void UpdateBook(Book item) => new BookDAO().UpdateBook(item);


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

        //-----------------------------------Book category------------------------------------------------//

        public int AddBookToCategory(Guid bookId, List<Guid> cateIds) => new BookDAO().AddBookToCategory(bookId, cateIds);


        public bool IsBookAlreadyInCate(Guid bookId, Guid cateId) => new BookDAO().IsBookAlreadyInCate(bookId, cateId);


        public int RemoveBookFromCate(Guid bookId, Guid cateId) => new BookDAO().RemoveBookFromCate(bookId, cateId);


        public List<Category> GetAllCategoryOfBook(Guid bookId) => new BookDAO().GetAllCategoryOfBook(bookId);
     
    }
}
