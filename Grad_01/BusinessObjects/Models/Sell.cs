using System;

namespace BusinessObjects.Models
{
    public class Sell
    {
        public Guid SellId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
        public DateTime SellDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual AppUser? Seller { get; set; }
    }
}
