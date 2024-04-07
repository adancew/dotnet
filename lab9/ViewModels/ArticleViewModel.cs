using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAppForRazorDemo.ViewModels;

    public enum Category
    {
        Food, Household, Clothing, Electronics, Other
    }

    public class ArticleViewModel
    {  

        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="To short name")]
        [Display(Name="Article name")]
        [MaxLength(20,ErrorMessage =" To long name, do not exceed {0}")]
        public string Name { get; set; }

	    public float Price { get; set; }

	    public Category Category { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpirationDate { get; set; } 

        public ArticleViewModel(){}

        public ArticleViewModel(int id, string name, float price, Category category, DateTime expirationDate)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.ExpirationDate = expirationDate;
        }
    }


