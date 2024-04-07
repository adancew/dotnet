using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista10.Models;

namespace lista10.Data
{
	public class ShopDbContext: DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options){}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories { get; set; }

		// nie trzeba, bo sam wywnioskuje jakie klucze obce są potrzebne
		//protected override void OnModelCreating(ModelBuilder modelBuilder){}
	}


}
