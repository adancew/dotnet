using Microsoft.AspNetCore.Mvc;
using lista10.ViewModels;
using Microsoft.AspNetCore.Http;

namespace lista10.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Game/Set/{set_value:int}")]
        public IActionResult Set(int set_value)
        {

            if (set_value <= 0)
            {
                HttpContext.Session.SetInt32("range", -1);
            }
            else
            {
                HttpContext.Session.SetInt32("range", set_value);
            }


            //ViewBag.range = HttpContext.Session.GetInt32("range");
            ViewBag.range = set_value;

            return View();
        }

        [HttpGet]
        [Route("Game/Guess/{guess_value1:int},{guess_value2:int}")]
        public IActionResult Guess(int guess_value1, int guess_value2)
        {
            int range = HttpContext.Session.GetInt32("range") ?? -1;
            int secret = HttpContext.Session.GetInt32("secret") ?? -1;

            ViewBag.values = (range_val: range,
                                secret_val: secret,
                                guess_val1: guess_value1,
                                guess_val2: guess_value2);

            ViewBag.results = GameViewModel.calculateWin(ViewBag.values);

            return View();
        }


        public IActionResult Draw()
        {
            int range = HttpContext.Session.GetInt32("range") ?? -1;
            int secret = HttpContext.Session.GetInt32("secret") ?? -1;

            if (range <= 0)
            {
                HttpContext.Session.SetInt32("secret", -1);
            }
            else
            {
                Random rnd = new Random();
                secret = rnd.Next(0, range);
                HttpContext.Session.SetInt32("secret", secret);
            }

            ViewBag.values = (range_val: range,
                                secret_val: secret);

            return View();
        }
    }



}
