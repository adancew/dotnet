using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lista12.Data;
using lista12.Models;

namespace lista12.Pages.Shopping
{
    public class DetailsModel : PageModel
    {
        private readonly lista12.Data.ShopDbContext _context;

        public DetailsModel(lista12.Data.ShopDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                var category = _context.Categories
                    .Where(c => c.CategoryId == article.CategoryId)
                    .FirstOrDefault();
                article.Category = category;

                Article = article;
            }
            return Page();
        }
    }
}
