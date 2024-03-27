using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using APIs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIs.Services.Intefaces;
using APIs.Utils.Paging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APIs.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("all")] // Adjusted route for clarity
        public IActionResult GetAllBooks([FromQuery] PagingParams @params)
        {
            try
            {
                var allbook = _bookService.GetAllBook(@params);


                if (allbook != null)
                {
                    var metadata = new
                    {
                        allbook.TotalCount,
                        allbook.PageSize,
                        allbook.CurrentPage,
                        allbook.TotalPages,
                        allbook.HasNext,
                        allbook.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(allbook);
                }
                else return BadRequest("No Books!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult GetBooksByName([FromQuery] string searchTerm, [FromQuery] PagingParams param)
        {
          try
            {
                var search = _bookService.GetAllBook(param);
                if(searchTerm != null && searchTerm != "")
                {
                    search = _bookService.GetBookByName(searchTerm, param);
                }
                if(search != null)
                {
                    var metadata = new
                    {
                        search.TotalCount,
                        search.PageSize,
                        search.CurrentPage,
                        search.TotalPages,
                        search.HasNext,
                        search.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(search);
                }
                return Ok(search);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //[HttpGet("search-by-cateName")]
        //public IActionResult GetBookByCategory([FromQuery] string[] cateName)
        //{
        //    if (cateName == null || cateName.Length == 0)
        //    {
        //        try
        //        {
        //            // Delegate to the BookService to retrieve all books
        //            List<Book> allBooks = _bookService.GetAllBook();
        //            return Ok(allBooks);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Consider logging the exception and returning a more user-friendly error message
        //            return StatusCode(500, "Internal Server Error");
        //        }
        //    }

        //    try
        //    {
        //        List<Book> booksByCategory = _bookService.GetBookByCategoryName(cateName);

        //        if (booksByCategory.Count == 0)
        //        {
        //            return NotFound($"No books found in the categories '{string.Join(", ", cateName)}'.");
        //        }

        //        return Ok(booksByCategory);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message); // Internal Server Error with message
        //    }
        //}

        [HttpGet("filter type")]
        public IActionResult GetBookByType([FromQuery] string type, [FromQuery] PagingParams param)
        {
            try
            {
                var types = _bookService.GetAllBook(param);
                if (type != null && type != "")
                {
                    types = _bookService.GetBookByName(type, param);
                }
                if (types != null)
                {
                    var metadata = new
                    {
                        types.TotalCount,
                        types.PageSize,
                        types.CurrentPage,
                        types.TotalPages,
                        types.HasNext,
                        types.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(types);
                }
                return Ok(types);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("filter")]
        public IActionResult FilterBooks( [FromQuery] string searchTerm = null,[FromQuery] string[] categoryNames = null,[FromQuery] string type = null)
        {
            try
            {
                List<Book> filteredBooks = _bookService.FilterBooks(searchTerm, categoryNames, type);
                if (filteredBooks.Count == 0)
                {
                    return NotFound("No books found matching the applied filters.");
                }
                return Ok(filteredBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error with message
            }
        }
        //-----------------------------------Book category------------------------------------------------//
        [HttpPost("category/add-book-to-category")]
        public IActionResult AddBookToCate(AddBookToCateDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Guid> validCates = new List<Guid>();
                    foreach (Guid id in dto.CateIds)
                    {
                        if (!_bookService.IsBookAlreadyInCate(dto.BookId, id))
                        {
                            validCates.Add(id);
                        }
                    }
                    if (validCates.Count == 0)
                    {
                        return Ok("This book's already in these/this cart(s)");
                    }
                    int changes = _bookService.AddBookToCategory(dto.BookId, validCates);
                    IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Add fail!");
                    return result;
                }
                return BadRequest("Model invalid!");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("category/remove-book-from-category")]
        public IActionResult RemoveBookFromCate(Guid bookId, Guid cateId)
        {
            try
            {
                int changes = _bookService.RemoveBookFromCate(bookId, cateId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("remove fail!");
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("category/get-all-cate-of-book")]
        public IActionResult GetAllCategoryOfBook(Guid bookId)
        {
            try
            {
                return Ok(_bookService.GetAllCategoryOfBook(bookId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /*    [HttpGet("search all")]
            public IActionResult SearchAll()
            {
                try
                {
                    List<Book> result = _bookService.GetAllBook();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }*/

    }
}
