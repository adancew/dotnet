using Microsoft.AspNetCore.Mvc;
using lista10.ViewModels;
using lista10.Data;
using lista10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace lista10.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public ShopController(ShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;

        }

        // GET: shop
        public async Task<IActionResult> Index()
        {
            // Populate the drop-down list with category names
            ViewBag.ItemNames = _context.Categories
                .Select(entity => entity)
                .ToList()
                .ConvertAll(item => new SelectListItem { Value = item.CategoryName, Text = item.CategoryName }); ;
           

            var shopDbContext = _context.Articles.Include(a => a.Category);
            return View(await shopDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult Index(string selectedItemName)
        {
            // Filter items based on the selected value
            List<Article> filteredItems;
           
            if (selectedItemName.IsNullOrEmpty())
            {
                filteredItems = _context.Articles.ToList();
            }
            else
            {
                filteredItems = _context.Articles
                    .Select(art => art)
                    .Where(art => art.Category.CategoryName == selectedItemName)
                    .ToList();
            }
            
            
            // Populate the drop-down list with category names
            ViewBag.ItemNames = _context.Categories
                .Select(entity => entity)
                .ToList()
                .ConvertAll(item => new SelectListItem { Value = item.CategoryName, Text = item.CategoryName }); ;


            return View(filteredItems);
        }



        // GET: shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
}
