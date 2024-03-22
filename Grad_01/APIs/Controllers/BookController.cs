using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using APIs.Services.Interfaces;
using System.Net;

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

        [HttpGet("search")]
        public IActionResult SearchProductsByName([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            try
            {
                List<SEODTO> searchResult = _bookService.ListSEO(searchTerm);

                if (searchResult.Count == 0)
                {
                    return NotFound("No products found with the provided search term.");
                }

                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error with message
            }
        }

        [HttpGet("get-all-book")]
        public IActionResult GetAllBook()
        {
            List<Book> result = new List<Book>();
            try
            {
                result = _bookService.GetAllBook();
                return Ok(result);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("get-product-by-id")]
        public IActionResult GetBookDetailsById(Guid bookId)
        {
            try
            {
                return Ok(_bookService.GetBookDetailsById(bookId));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
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
                    if(validCates.Count == 0)
                    {
                        return Ok("This book's already in these/this cart(s)");
                    }
                    int changes = _bookService.AddBookToCategory(dto.BookId, validCates);
                    IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Add fail!");
                    return result;
                }
                return BadRequest("Model invalid!");

            }
            catch(Exception e)
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
