﻿using System.Reflection.Emit;
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

        //Subscribtion services DbSets
        public virtual DbSet<Tier> Tiers { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<SubRecord> SubRecords { get; set; } = null!;


        //Rating services DbSets
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<RatingRecord> RatingRecords { get; set; } = null!;

        //Payment service DbSets 
        //public virtual DbSet<PaymentDetails> PaymentDetails { get; set; } = null!;
        public virtual DbSet<TransactionRecord> Transactions { get; set; } = null!;
        //public virtual Db

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
            builder.Entity<Role>().HasData(
                new Role {
                    RoleId = Guid.Parse("2da9143d-559c-40b5-907d-0d9c8d714c6c"),
                    RoleName = "BaseUser",
                    Description = "Role for base user?"
                }
                );
            base.OnModelCreating(builder); ;
            builder.Entity<Inventory>().HasNoKey();
            builder.Entity<Basket>().HasNoKey();
            builder.Entity<RatingRecord>().HasNoKey();
            builder.Entity<CategoryList>().HasNoKey();
            builder.Entity<CICMedia>().HasNoKey();

            builder.Entity<SubRecord>()
           .HasOne(sr => sr.Subscription)
           .WithMany()
           .HasForeignKey(sr => sr.SubscriptionId)
           .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<SubscriptionModel>()
            //.HasOne(s => s.TierList)
            //.WithMany()
            //.HasForeignKey(s => s.TierId)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}

