using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using APIs.Services.Interfaces;
using DataAccess.DAO;

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

        // Book CRUD Endpoints with DTOs

        [HttpPost("AddBook")]
        public IActionResult AddNewBook([FromBody] BookDetailsDTO item)
        {
            try
            {
                _bookService.AddNewBook(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook([FromBody] BookDetailsDTO item)
        {
            try
            {
                _bookService.UpdateBook(item);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteBook/{bookId}")]
        public IActionResult DeleteBook(Guid bookId)
        {
            try
            {
                _bookService.DeleteBook(bookId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
