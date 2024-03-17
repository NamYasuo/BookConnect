using BookConnectAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookConnectAPI.Data
{
    public class SellerDbContext : DbContext
    {
        public DbSet<Seller> Sellers { get; set; }

        public SellerDbContext(DbContextOptions<SellerDbContext> options) : base(options)
        {
        }
    }

}
