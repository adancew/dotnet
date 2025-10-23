using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lista12.Models
{
	public class Article
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

        [NotMapped]
        public IFormFile? FormFile { get; set; }

		public String Filename { get; set; }

		public bool IsPromo { get; set; }

        public Article() { }

		public Article(int id, string name, float price, Category category, 
			IFormFile formFile, string Filename, bool isPromo)
		{
			this.Id = id;
			this.Name = name;
			this.Price = price;
			this.Category = category;
			this.FormFile = formFile;
			this.Filename = Filename;
			this.IsPromo = isPromo;
		}

	}
}
