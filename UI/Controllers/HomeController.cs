using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult SetCulture(string culture)
        {
            string controller = null;
            string action = null;
            string area = null;
            int? id = null;

            if (TempData["Controller"] != null)
            {
                controller = TempData["Controller"].ToString();

                if (TempData["Controller"].ToString() != "Authentication")
                {
                    TempData["Area"] = "";
                }
            }
            else
            {
                controller = "Home";
            }

            if (TempData["Action"] != null)
            {
                action = TempData["Action"].ToString();
            }
            else
            {
                action = "Index";
            }

            if (TempData["Area"] != null)
            {
                area = TempData["Area"].ToString();
            }
            else
            {
                area = "";
            }

            if (TempData["Id"] != null)
            {
                id = int.Parse(TempData["Id"].ToString());
            }
            else
            {
                id = null;
            }

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(5)
            };
            Request.HttpContext.Response.Cookies.Append("culture", culture, cookieOptions);
            Request.HttpContext.Session.SetString("culture", culture);
            return RedirectToAction(action, controller, new { area, id });
        }

        public IActionResult Index()
        {
            return View();
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
