using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using APIs.Services.Interfaces;

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
    }
}
