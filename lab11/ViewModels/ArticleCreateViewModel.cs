using lista10.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lista10.ViewModels
{
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [Display(Name = "Article name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {0}")]
        public string? Name { get; set; }

        public float Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IFormFile? FormFile { get; set; }

        public ArticleCreateViewModel() { }

        public ArticleCreateViewModel(int id, string name, float price, Category category, IFormFile FormFile)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.FormFile = FormFile;
        }

    }
}
