using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista12.Models;
using WebAppForEntityFrameworkDemo.Data;


namespace lista12.Data
{
	public class ShopDbContext: DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options){}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }


}
