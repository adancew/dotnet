using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lista12.Data;
using lista12.Models;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

namespace lista12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly ShopDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        public SelectList Categories { get; set; }

        public CreateModel(lista12.Data.ShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;

            var distinctCategories = context.Categories.Select(c => c.CategoryName).Distinct().ToList();
            Categories = new SelectList(distinctCategories);
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }


        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public float Price { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }
        [BindProperty]
        public bool IsPromo { get; set; }
        [BindProperty]
        public IFormFile FormFile { get; set; }
       
        public async Task<IActionResult> OnPostAsync()
        {

                string upload_folder = Path.Combine(_hostEnvironment.WebRootPath, "upload");
                string filename = "no_image.jpg";

                if (!Directory.Exists(upload_folder))
                {
                    Directory.CreateDirectory(upload_folder);
                }

                if (FormFile != null)
                {
                    // save image to disc
                    filename = 
                       Guid.NewGuid().ToString() +
                       Path.GetExtension(FormFile.FileName);


                    using (Stream fileStream = new FileStream(Path.Combine(upload_folder, filename), FileMode.Create))
                    {
                        FormFile.CopyTo(fileStream);
                    }

                }

                var category = _context.Categories
                    .Where(c => c.CategoryName ==  CategoryName)
                    .FirstOrDefault();
                // save article to database
                Article article = new Article()
                {
                    Id = Id,
                    Name = Name,
                    Price = Price,
                    CategoryId = category?.CategoryId ?? -1,
                    Category = category,
                    FormFile = FormFile,
                    Filename = filename,
                    IsPromo = IsPromo,
                };
                _context.Add(article);
                await _context.SaveChangesAsync();

                return RedirectToPage(nameof(Index));
         
        }
    }
}
