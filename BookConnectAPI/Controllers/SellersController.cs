using BookConnectAPI.Data;
using BookConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookConnectAPI.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellersController : ControllerBase
    {
        private readonly SellerDbContext _context;

        public SellersController(SellerDbContext context)
        {
            _context = context;
        }

        // GET: api/sellers
        [HttpGet]
        public ActionResult<IEnumerable<Seller>> GetSellers()
        {
            return _context.Sellers.ToList();
        }

        // GET: api/sellers/5
        [HttpGet("{id}")]
        public ActionResult<Seller> GetSeller(int id)
        {
            var seller = _context.Sellers.Find(id);

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }

        // POST: api/sellers
        [HttpPost]
        public ActionResult<Seller> CreateSeller(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sellers.Add(seller);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSeller), new { id = seller.SellerId }, seller);
        }

        // PUT: api/sellers/5
        [HttpPut("{id}")]
        public IActionResult UpdateSeller(int id, Seller seller)
        {
            if (id != seller.SellerId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(seller).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/sellers/5
        [HttpDelete("{id}")]
        public ActionResult<Seller> DeleteSeller(int id)
        {
            var seller = _context.Sellers.Find(id);

            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            _context.SaveChanges();

            return seller;
        }
    }

}
