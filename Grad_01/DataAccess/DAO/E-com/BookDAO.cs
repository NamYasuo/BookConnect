using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

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
				using(var context = new AppDbContext())
				{
					bookList = context.Books.ToList();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
			return bookList;
		}

		//Get book by id
		public Book GetBookById(Guid bookId)
		{
			Book? book = new Book();
			try
			{
				using(var context = new AppDbContext())
				{
					book = context.Books.Where(b => b.ProductId == bookId).FirstOrDefault();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
			if (book != null) return book;
			else throw new NullReferenceException();
		}

        


        //Add new book
        public void AddNewBook(Book book)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Add(book);
					context.SaveChanges();
				}

			}catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		//Modify book
		public void UpdateBook(Book book)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Books.Update(book);
					context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		//Delete book by id
		public void DeleteBook(Guid bookId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Book book = GetBookById(bookId);
					if(book != null)
					{
						context.Books.Remove(book);
						context.SaveChanges();
                    }
                }
			}
			catch(Exception e)
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
					foreach(Guid i in bookIds)
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
        //Get book by name
        public List<Book> GetBookByName(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new ArgumentException("Search term cannot be empty.");
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    return context.Books.Where(b => b.Name.Contains(searchTerm)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Book> GetBookByCategoryName(string[] cateName)
        {
            List<Book> bookList = new List<Book>();
            try
            {
                using (var context = new AppDbContext())
                {
                    // Build the raw SQL query using string interpolation
                    string sql = $@"
                SELECT DISTINCT b.*
                FROM Books AS b
                LEFT JOIN CategoryLists AS cl ON b.ProductId = cl.BookId
                LEFT JOIN Categories AS c ON cl.CategoryId = c.CateId
                WHERE c.CateName IN ({string.Join(",", cateName.Select(x => "'" + x + "'"))})
            ";

                    bookList = context.Set<Book>().FromSqlRaw(sql).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookList;
        }
       

            public List<Book> GetBookByType(string type)
            {
                List<Book> bookList = new List<Book>();
                try
                {
                    using (var context = new AppDbContext())
                    {
                        // Validate the type input (optional but recommended)
                        if (type != "OLD" && type != "NEW")
                        {
                            throw new ArgumentException("Invalid book type. Valid types are 'OLD' and 'NEW'");
                        }

                        bookList = context.Books.Where(b => b.Type == type).ToList();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return bookList;
            }
        }







    
}

