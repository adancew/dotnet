using lista10.Models;
using lista10.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace lista10.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

        public void SetCookie(string key, string value, int? numberOfSeconds = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfSeconds.HasValue)
                option.Expires = DateTime.Now.AddSeconds(numberOfSeconds.Value);
            Response.Cookies.Append(key, value, option);
        }

        public IActionResult Index()
		{
			var usertype = Request.Cookies["usertype"];

			if (usertype.IsNullOrEmpty())
			{
				usertype = "client";
                SetCookie("usertype", usertype, 600);
            }

            ViewData["usertype"] = usertype;

            return View();
		}

        [HttpPost]
        public IActionResult Index(bool checkResp)
        {
			if (checkResp)
			{
                SetCookie("usertype", "owner", 600);
            }
			else
			{
                SetCookie("usertype", "client", 600);
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
