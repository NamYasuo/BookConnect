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

        [HttpGet("search")]
        public IActionResult SearchProductsByName()
        {
            try{
                List<SEODTO> result = _bookService.ListSEO();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
