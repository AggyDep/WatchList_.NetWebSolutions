using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Models.Actor;

namespace UI.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ActorsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<ActorVM> actors;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                actors = await JsonSerializer.DeserializeAsync<IEnumerable<ActorVM>>(responseStream);
            }
            else
            {
                actors = Array.Empty<ActorVM>();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(a => a.FullName.Contains(searchString));
            }

            return View(actors);
        }

        public async Task<IActionResult> Details(int id)
        {
            ActorVM actor;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                actor = await JsonSerializer.DeserializeAsync<ActorVM>(responseStream);
            }
            else
            {
                actor = new ActorVM();
            }

            return View(actor);
        }

        public async Task<IActionResult> Create()
        {
            if (!HttpContext.Session.Keys.Contains("users"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors");
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
        public async Task<IActionResult> Create(ActorPostVM actorPost)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var actorContent = new StringContent(JsonSerializer.Serialize(actorPost), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri("http://localhost:55169/api/Actors"), actorContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(actorPost);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ActorPutVM actor;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                actor = await JsonSerializer.DeserializeAsync<ActorPutVM>(responseStream);
            }
            else
            {
                actor = new ActorPutVM();
            }

            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActorPutVM actorPut)
        {
            if (actorPut.MovieActorDTOs == null)
            {
                actorPut.MovieActorDTOs = new MovieActorVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var actorContent = new StringContent(JsonSerializer.Serialize(actorPut), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Actors/" + id), actorContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(actorPut);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ActorDeleteVM actor;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                actor = await JsonSerializer.DeserializeAsync<ActorDeleteVM>(responseStream);
            }
            else
            {
                actor = new ActorDeleteVM();
            }

            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ActorDeleteVM actorDelete)
        {
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(new Uri("http://localhost:55169/api/Actors/" + id)).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(actorDelete);
        }
    }
}