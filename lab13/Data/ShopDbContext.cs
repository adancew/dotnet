using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace lista10.Data
{
	public class ShopDbContext: IdentityDbContext
    {
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options){}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories { get; set; }


		
		protected override void OnModelCreating(ModelBuilder modelBuilder){
			base.OnModelCreating(modelBuilder);
			//modelBuilder.Seed();
		}
		

	}


}
