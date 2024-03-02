using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Ecom;
using BusinessObjects.Models.Ecom.Payment;
using BusinessObjects.Models.Ecom.Rating;
using BusinessObjects.Models.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
	public class AppDbContext: DbContext
	{
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> o) : base(o) { }

        //Base services DbSets
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Agency> Agencies { get; set; } = null!;
        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;


        //Rating services DbSets
        public virtual DbSet<RatingRecord> RatingRecords { get; set; } = null!;

        //Payment service DbSets 
        public virtual DbSet<PaymentDetails> PaymentDetails { get; set; } = null!;

        //Trading services DbSets
        public virtual DbSet<Post> Posts { get; set; } = null!;

        //Utility DbSets
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TokenInfo> TokenInfos { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryList> CategoryLists { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<CICMedia> CICMedias { get; set; } = null!;


        //Creative services DbSet
        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<Work> Works { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); ;
            builder.Entity<Inventory>().HasNoKey();
            builder.Entity<Basket>().HasNoKey();
            builder.Entity<RatingRecord>().HasNoKey();
            builder.Entity<CategoryList>().HasNoKey();
            builder.Entity<CICMedia>().HasNoKey();
        }
    }
}

