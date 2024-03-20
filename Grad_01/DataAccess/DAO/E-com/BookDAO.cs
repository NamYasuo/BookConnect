﻿using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.DAO.Ecom;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        //Get all book
        public List<Book> GetAllBook()
        {

            List<Book> bookList = new List<Book>();
            try
            {
                using (var context = new AppDbContext())
                {
                    bookList = context.Books.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookList;
        }

        //Get book details by id

        public BookDetailsDTO GetBookDetailsById(Guid bookId)
        {
            Book? book = new Book();
            BookDetailsDTO result = new BookDetailsDTO();


            try
            {
                using (var context = new AppDbContext())
                {
                    book = context.Books.Where(b => b.ProductId == bookId).FirstOrDefault();
                }
                if (book != null)
                {
                    NameAndIdDTO agency = new AgencyDAO().GetNameAndId(bookId);
                    result = new BookDetailsDTO()
                    {
                        ProductId = bookId,
                        //public string? Name { get; set; } = null!;
                        Name = book.Name,
                        //public string? Description { get; set; }
                        Description = book.Description,
                        //public double? Price { get; set; }
                        Price = book.Price,
                        //public DateTime? CreatedDate { get; set; }
                        CreatedDate = book.CreatedDate,
                        //public DateTime? PublishDate { get; set; }
                        PublishDate = book.PublishDate,
                        //public string? Type { get; set; } = null!;
                        Type = book.Type,
                        //public int? Quantity { get; set; }
                        //public double Rating { get; set; }
                        Rating = 5,
                        //public Guid SellerId { get; set; }
                        AgencyId = agency.AgencyId,
                        //public string SellerName { get; set; } = null!;
                        AgencyName = agency.AgencyName
                    };
                }
                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Get book by id

        public Book GetBookById(Guid bookId)
        {
            Book? book = new Book();
            try
            {
                using (var context = new AppDbContext())
                {
                    book = context.Books.Where(b => b.ProductId == bookId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (book != null) return book;
            else throw new NullReferenceException();
        }

        //Get book by name
        public Book GetBookByName(string name)
        {
            Book? book = new Book();
            try
            {
                using (var context = new AppDbContext())
                {
                    book = context.Books.Where(b => b.Name == name).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (book != null) return book;
            else throw new NullReferenceException();
        }

        //Add new book
        public void AddNewBook(BookDetailsDTO item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var entity = new Book
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Price = (decimal)item.Price,
                        CreatedDate = DateTime.Now,
                        PublishDate = item.PublishDate,
                        CoverDir = item.CoverDir,
                        BackgroundDir = item.BackgroundDir,
                        Type = item.Type,
                        Author = item.Author
                    };
                    context.Books.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Modify book
        public void UpdateBook(BookDetailsDTO item)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingItem = context.Books.FirstOrDefault(i => i.ProductId == item.ProductId);
                    if (existingItem != null)
                    {
                        existingItem.ProductId = item.ProductId;
                        existingItem.Name = item.Name;
                        existingItem.Description = item.Description;
                        existingItem.CreatedDate = DateTime.Now;
                        existingItem.PublishDate = item.PublishDate;
                        existingItem.CoverDir = item.CoverDir;
                        existingItem.BackgroundDir = item.BackgroundDir;
                        existingItem.Author = item.Author;
                        existingItem.Type = item.Type;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Delete book by id
        public void DeleteBook(Guid bookId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Book book = GetBookById(bookId);
                    if (book != null)
                    {
                        context.Books.Remove(book);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Book> GetBookListById(List<Guid> bookIds)
        {
            List<Book> listBooks = new List<Book>();
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach (Guid i in bookIds)
                    {
                        listBooks.Add(context.Books.Where(b => b.ProductId == i).FirstOrDefault());

                    }
                    return listBooks;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddNewBook(Book item)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book item)
        {
            throw new NotImplementedException();
        }
    }
}

