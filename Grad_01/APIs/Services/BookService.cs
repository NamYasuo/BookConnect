using System;
using System.Collections.Generic;

using APIs.Services.Interfaces;

using BusinessObjects.Models;
using DataAccess.DAO;
using BusinessObjects.DTO;
using BusinessObjects;
using APIs.Utils.Paging;
namespace APIs.Services
{
    public class BookService : IBookService
    {



        public PagedList<Book> GetAllBook(PagingParams param)
        {
            return PagedList<Book>.ToPagedList(new BookDAO().GetAllBook().OrderBy(c => c.Name).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public List<Book> GetAllBook() => new BookDAO().GetAllBook();



        public Book GetBookById(Guid bookId) => new BookDAO().GetBookById(bookId);




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

        //-----------------------------------Book category------------------------------------------------//

        public int AddBookToCategory(Guid bookId, List<Guid> cateIds) => new BookDAO().AddBookToCategory(bookId, cateIds);


        public bool IsBookAlreadyInCate(Guid bookId, Guid cateId) => new BookDAO().IsBookAlreadyInCate(bookId, cateId);


        public int RemoveBookFromCate(Guid bookId, Guid cateId) => new BookDAO().RemoveBookFromCate(bookId, cateId);


        public List<Category> GetAllCategoryOfBook(Guid bookId) => new BookDAO().GetAllCategoryOfBook(bookId);



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

        public PagedList<Book> GetBookByType(string type, PagingParams param)
        {
            return PagedList<Book>.ToPagedList(new BookDAO().GetBookByType(type).OrderBy(c => c.Name).AsQueryable(), param.PageNumber, param.PageSize);
        }




        public PagedList<Book> GetBookByName(string searchTerm, PagingParams param)
        {
            return PagedList<Book>.ToPagedList(new BookDAO().GetBookByName(searchTerm).OrderBy(c => c.Name).AsQueryable(), param.PageNumber, param.PageSize);

        }

        public List<Book> FilterBooks(string searchTerm = null, string[] categoryNames = null, string type = null)
        {
            throw new NotImplementedException();
        }

       
    }

        //public List<Book> FilterBooks(string searchTerm = null, string[] categoryNames = null, string type = null)
        //{
        //    List<Book> filteredBooks = new List<Book>();

        //    // Filter by name (optional)
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        filteredBooks = GetBookByName(searchTerm);
        //    }
        //    else
        //    {
        //        filteredBooks = GetAllBook(); // Get all books if no search term
        //    }

        //    // Filter by category names (optional)
        //    if (categoryNames != null && categoryNames.Any())
        //    {
        //        filteredBooks = GetBookByCategoryName(categoryNames).Intersect(filteredBooks).ToList();
        //    }

        //    // Filter by type (optional)
        //    if (type != null && (type == "OLD" || type == "NEW"))
        //    {
        //        filteredBooks = GetBookByType(type).Intersect(filteredBooks).ToList();
        //    }

        //    return filteredBooks;
        //}
        
    }


