using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using APIs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIs.Services.Intefaces;

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
        public IActionResult GetAllBooks()
        {
            try
            {
                List<Book> allBooks = _bookService.GetAllBook();
                return Ok(allBooks);
            }
            catch (Exception ex)
            {
                // Consider logging the exception and returning a more user-friendly error message
                return StatusCode(500, "Internal Server Error");
            }
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
                List<Book> searchResult = _bookService.GetBookByName(searchTerm);

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

        [HttpGet("search-by-cateName")]
        public IActionResult GetBookByCategory([FromQuery] string[] cateName)
        {
            if (cateName == null || cateName.Length == 0)
            {
                try
                {
                    // Delegate to the BookService to retrieve all books
                    List<Book> allBooks = _bookService.GetAllBook();
                    return Ok(allBooks);
                }
                catch (Exception ex)
                {
                    // Consider logging the exception and returning a more user-friendly error message
                    return StatusCode(500, "Internal Server Error");
                }
            }

            try
            {
                List<Book> booksByCategory = _bookService.GetBookByCategoryName(cateName);

                if (booksByCategory.Count == 0)
                {
                    return NotFound($"No books found in the categories '{string.Join(", ", cateName)}'.");
                }

                return Ok(booksByCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error with message
            }
        }

        [HttpGet("filter type")]
        public IActionResult GetBookByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type parameter cannot be empty.");
            }

            try
            {
                List<Book> booksByType = _bookService.GetBookByType(type);

                if (booksByType.Count == 0)
                {
                    return NotFound($"No books found of type '{type}'.");
                }

                return Ok(booksByType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error with message
            }
        }

        [HttpGet] // Adjust HTTP method as needed
        [Route("api/books/search")] // Adjust route as needed
        public IActionResult GetBook(string searchTerm, [FromQuery] string[] cateName, string type)
        {
            try
            {
                List<Book> searchResult = new List<Book>();

                // Apply filters individually and collect unique results
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var booksByName = _bookService.GetBookByName(searchTerm);
                    searchResult.AddRange(booksByName.Except(searchResult)); // Add books not already present
                }

                if (cateName != null && cateName.Length > 0)
                {
                    var booksByCategory = _bookService.GetBookByCategoryName(cateName);
                    searchResult.AddRange(booksByCategory.Except(searchResult)); // Add books not already present
                }

                if (!string.IsNullOrEmpty(type))
                {
                    var booksByType = _bookService.GetBookByType(type);
                    searchResult.AddRange(booksByType.Except(searchResult)); // Add books not already present
                }

                // Return appropriate response based on results
                if (searchResult.Count == 0)
                {
                    return NotFound("No books found based on the provided search criteria.");
                }

                return Ok(searchResult); // Return the filtered books
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during book search: {ex.Message}");
                // Log the exception (consider a dedicated logging framework)
                return StatusCode(500, "Internal Server Error"); // Handle exceptions gracefully
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
