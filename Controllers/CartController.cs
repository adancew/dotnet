using Azure.Core;
using lista10.Data;
using lista10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace lista10.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private IWebHostEnvironment _hostEnvironment;
        private int WEEK_SECS = 7 * 24 * 60 * 60;

        public CartController(ShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            // get list of article objects paired with amount stored in the cookie
            // what if the article no longer exists?
            /*
            var cart_cookies = Request.Cookies
                .Where(cookie => cookie.Key.StartsWith("article"))
                .Select(cookie => new Tuple<Article, int>
                (
                    _context.Articles
                        .Where(article => article.Id == Int32.Parse(cookie.Key.Remove(0, 7)))
                        .ToList()[0],
                    Int32.Parse(cookie.Value)
                ))
                .ToList();
            */

            var raw_cookies = Request.Cookies
                .Where(cookie => cookie.Key.StartsWith("article"));

            var cart_cookies = new List<Tuple<Article, int>>();
            
            foreach(var cookie in raw_cookies)
            {
                
                var search_res = _context.Articles
                       .Where(article => article.Id == Int32.Parse(cookie.Key.Remove(0, 7)));

                if (!search_res.IsNullOrEmpty())
                {
                    var article = search_res.ToList()[0];
                    cart_cookies.Add(Tuple.Create(article, Int32.Parse(cookie.Value)));
                }
                    
                       
            }
   

            return View(cart_cookies);

        }

        public void SetCookie(string key, string value, int? numberOfSeconds = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }

        
        public async Task<IActionResult> RemoveCart(int? id)
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

            if (!cookie_result.IsNullOrEmpty()) { 
                var my_cookie = cookie_result.ToList()[0];
                SetCookie(my_cookie.Key, (Int32.Parse(my_cookie.Value) + 1).ToString(), -1000);
            }

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var cookie_result = Request.Cookies
                .Where(cookie => cookie.Key.Equals($"article{id}"))
                .Select(cookie => cookie)
                .ToList();

            if (!cookie_result.IsNullOrEmpty())
            {
                var my_cookie = cookie_result.ToList()[0];
                SetCookie(my_cookie.Key, (Int32.Parse(my_cookie.Value) + 1).ToString(), WEEK_SECS);
            }

            return RedirectToAction("Index", "Cart");
        }


        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            var cookie_result = Request.Cookies
                .Where(cookie => cookie.Key.Equals($"article{id}"))
                .Select(cookie => cookie)
                .ToList();

            if (!cookie_result.IsNullOrEmpty())
            {
                var my_cookie = cookie_result.ToList()[0];

                if (Int32.Parse(my_cookie.Value) > 1)
                    SetCookie(my_cookie.Key, (Int32.Parse(my_cookie.Value) - 1).ToString(), WEEK_SECS);
                else
                    SetCookie(my_cookie.Key, "", -1000);
            }

            return RedirectToAction("Index", "Cart");
        }
    }


   
}
