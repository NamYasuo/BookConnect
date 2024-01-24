using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("search")]
        public IActionResult SearchProductsByName(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = _dbContext.Books
                .Where(p => p.Name != null && p.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            return Ok(result);
        }
    }
}
