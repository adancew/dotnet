using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lista12.Data;
using lista12.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace lista12.Pages.Shopping
{
    public class IndexModel : PageModel
    {
        private readonly lista12.Data.ShopDbContext _context;

        private readonly List<Article> _allItems; 
        public List<Article> FilteredItems { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }


        public IndexModel(lista12.Data.ShopDbContext context)
        {
            _context = context;

            _allItems = context.Articles.ToList();
            FilteredItems = _allItems;

            var distinctCategories = context.Categories.Select(c => c.CategoryName).Distinct().ToList();
            
            Categories = new SelectList(distinctCategories);
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(SelectedCategory)) {
                FilteredItems = _allItems;
            }
            else {
                var catId = _context.Categories
                            .Where(c => c.CategoryName.Equals(SelectedCategory))
                            .Select(c => c.CategoryId).ToList()[0];

                FilteredItems = _allItems
                    .Where(item => item.CategoryId == catId)
                    .ToList();
            }
        }

    }
}
