using Microsoft.AspNetCore.Mvc;
using lista10.ViewModels;
using lista10.Data;
using lista10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace lista10.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        private int WEEK_SECS = 7*24*60*60;

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

        public void SetCookie(string key, string value, int? numberOfSeconds = null)
        {
            CookieOptions option = new CookieOptions();
            if(numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }


        // GET: shop/
        public async Task<IActionResult> AddCart(int? id)
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

            var cookie_result = Request.Cookies
                .Where(cookie => cookie.Key.Equals($"article{id}"))
                .Select(cookie => cookie)
                .ToList();

            if (cookie_result.IsNullOrEmpty()){
                SetCookie($"article{id}", 1.ToString(), WEEK_SECS);
            }
            else {
                var my_cookie = cookie_result.ToList()[0];
                SetCookie(my_cookie.Key, (Int32.Parse(my_cookie.Value) + 1).ToString(), WEEK_SECS);
            }

            return RedirectToAction("Index", "Shop");
        }
    }
}
