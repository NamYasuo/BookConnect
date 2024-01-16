using System;
using System.Reflection.Emit;
using APIs.DTO.Ecom;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.Ecom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
	public class AppDbContext: DbContext
	{
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> o) : base(o) { }

        //Base DbSet
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Agency> Agencies { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<ListProducts> ListProducts { get; set; } = null!;
        public virtual DbSet<TokenInfo> TokenInfos { get; set; } = null!;


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
            builder.Entity<ProductDetail>().HasNoKey();
            builder.Entity<ListProducts>().HasNoKey();
        }
    }
}

