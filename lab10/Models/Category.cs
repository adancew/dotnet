using System.ComponentModel.DataAnnotations;

namespace lista10.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		
		[Required]
		[MinLength(2, ErrorMessage = "To short name")]
		[Display(Name = "Category name")]
		[MaxLength(40, ErrorMessage = " To long name, do not exceed {0}")]
		public string CategoryName { get; set; } 

		public ICollection<Article>? Articles { get; set; }
		
	}
}
