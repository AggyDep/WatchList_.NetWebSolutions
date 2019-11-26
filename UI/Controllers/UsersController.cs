using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using UI.Models;
using System.Text.Json;
using System.Text;

namespace UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserVM> users;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                users = await JsonSerializer.DeserializeAsync<IEnumerable<UserVM>>(responseStream);
            }
            else
            {
                users = Array.Empty<UserVM>();
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            UserVM user;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                user = await JsonSerializer.DeserializeAsync<UserVM>(responseStream);
            }
            else
            {
                user = new UserVM();
            }

            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            if (!HttpContext.Session.Keys.Contains("users"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users");
                request.Headers.Add("Accept", "application/json");

                var client = _httpClientFactory.CreateClient();

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("users", responseString);
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserPostVM userPost)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var userContent = new StringContent(JsonSerializer.Serialize(userPost), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri("http://localhost:55169/api/Users"), userContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(userPost);
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserPutVM user;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                user = await JsonSerializer.DeserializeAsync<UserPutVM>(responseStream);
            }
            else
            {
                user = new UserPutVM();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserPutVM userPut)
        {
            if (userPut.WatchListDTOs == null)
            {
                userPut.WatchListDTOs= new WatchListVM[0];
            }
            if (userPut.UserFriendsDTOs == null)
            {
                userPut.UserFriendsDTOs = new UserFriendVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var userContent = new StringContent(JsonSerializer.Serialize(userPut), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Users/" + id), userContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(userPut);
        }

        public async Task<IActionResult> Delete(string id)
        {
           UserDeleteVM user;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                user = await JsonSerializer.DeserializeAsync<UserDeleteVM>(responseStream);
            }
            else
            {
                user = new UserDeleteVM();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, UserDeleteVM userDelete)
        {
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(new Uri("http://localhost:55169/api/Users/" + id)).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(userDelete);
        }
    }
}