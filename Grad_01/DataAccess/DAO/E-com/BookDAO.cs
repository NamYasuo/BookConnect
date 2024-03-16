using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using BusinessObjects.Models.Utils;
using DataAccess.DAO.Ecom;
using Microsoft.Data.SqlClient;
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

        //Get book details by id

        public BookDetailsDTO GetBookDetailsById(Guid bookId)
		{
			Book? book = new Book();
			BookDetailsDTO result = new BookDetailsDTO();


            try
			{
				using(var context = new AppDbContext())
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
				} return result;
              
			}
			catch(Exception e)
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


		//-----------------------------------Book category------------------------------------------------//

		public int AddBookToCategory(Guid bookId, Guid cateId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
                    string insertQuery = $"insert into CategoryLists(CategoryId, BookId) values (@cateId, @bookId)";

					int result = context.Database.ExecuteSqlRaw(insertQuery,
						new SqlParameter("@cateId", cateId),
						new SqlParameter("@bookId", bookId));
					return result;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<Category> GetAllCategoryOfBook(Guid bookId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					List<Category> result = new List<Category>();
					List<CategoryList> listCate = context.CategoryLists.Where(o => o.BookId == bookId).ToList();
					foreach(CategoryList c in listCate)
					{
						var cate = context.Categories.Where(d => d.CateId == c.CategoryId).SingleOrDefault();
						if(cate != null)
						{
                            result.Add(cate);
                        }
                    }
					return result;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool IsBookAlreadyInCate(Guid bookId, Guid cateId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					return context.CategoryLists.Any(c => c.BookId == bookId && c.CategoryId == cateId);
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public int RemoveBookFromCate(Guid bookId, Guid cateId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
                    string insertQuery = $"delete from CategoryLists where CategoryId = @cateId and  BookId = @bookId";

                    int result = context.Database.ExecuteSqlRaw(insertQuery,
                        new SqlParameter("@cateId", cateId),
                        new SqlParameter("@bookId", bookId));
                    return result;
                }
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}

