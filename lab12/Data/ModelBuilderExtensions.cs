using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista12.Models;

namespace WebAppForEntityFrameworkDemo.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                 new Category()
                 {
                     CategoryId = 1,
                     CategoryName = "category1"
                 },
                 new Category()
                 {
                     CategoryId = 2,
                     CategoryName = "category2"
                 },
                 new Category()
                 {
                     CategoryId = 3,
                     CategoryName = "category3"
                 }
                );


            modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    Id = 1,
                    Name = "article1",
                    Price = 12,
                    CategoryId = 3,
                    Filename = "puppy1.jpg"
                },
                new Article()
                {
                    Id = 2,
                    Name = "article2",
                    Price = 2,
                    CategoryId = 2,
                  
                    Filename = "puppy2.jpg"
                },
                new Article()
                {
                    Id = 3,
                    Name = "article3",
                    Price = 24,
                    CategoryId = 3,
                   
                    Filename = "puppy3.jpg"
                },
                new Article()
                {
                    Id = 4,
                    Name = "article4",
                    Price = 3,
                    CategoryId = 2,
                   
                    Filename = "puppy4.jpg"
                },
                new Article()
                {
                    Id = 5,
                    Name = "article5",
                    Price = 42,
                    CategoryId = 3,
                    
                    Filename = "puppy5.jpg"
                }

                ); ;

        }
    }
}
