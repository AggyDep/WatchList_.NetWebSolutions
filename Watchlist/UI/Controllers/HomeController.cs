using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.Keys.Contains("movies"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Movies");
                request.Headers.Add("Accept", "application/json");

                var client = _httpClientFactory.CreateClient();

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("movies", responseString);
                }
            }

            if (!HttpContext.Session.Keys.Contains("watchlist"))
            {
                var userId = TempData["Id"];
                using var watchListRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + userId + "/watchlist");
                watchListRequest.Headers.Add("Accept", "application/json");

                var watchListClient = _httpClientFactory.CreateClient();
                var response = await watchListClient.SendAsync(watchListRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("watchlist", responseString);
                }
            }
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
