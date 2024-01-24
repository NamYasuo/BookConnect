using System;
using BusinessObjects;
using BusinessObjects.Models;

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
	}
}

