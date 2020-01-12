using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Models.Genre;

namespace UI.Controllers
{
    public class GenresController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GenresController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GenreVM> genres;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                genres = await JsonSerializer.DeserializeAsync<IEnumerable<GenreVM>>(responseStream);
            }
            else
            {
                genres = Array.Empty<GenreVM>();
            }

            return View(genres);
        }

        public async Task<IActionResult> Details(int id)
        {
            GenreVM genre;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                genre = await JsonSerializer.DeserializeAsync<GenreVM>(responseStream);
            }
            else
            {
                genre = new GenreVM();
            }

            return View(genre);
        }

        public async Task<IActionResult> Create()
        {
            if (!HttpContext.Session.Keys.Contains("users"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres");
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
        public async Task<IActionResult> Create(GenrePostVM genrePost)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var genreContent = new StringContent(JsonSerializer.Serialize(genrePost), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri("http://localhost:55169/api/Genres"), genreContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(genrePost);
        }

        public async Task<IActionResult> Edit(int id)
        {
            GenreVM genre;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                genre = await JsonSerializer.DeserializeAsync<GenreVM>(responseStream);
            }
            else
            {
                genre = new GenreVM();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreVM genre)
        {
            if (genre.MovieGenreDTOs == null)
            {
                genre.MovieGenreDTOs = new MovieGenreVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var genreContent = new StringContent(JsonSerializer.Serialize(genre), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Genres/" + id), genreContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(genre);
        }

        public async Task<IActionResult> Delete(int id)
        {
            GenreVM genre;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                genre = await JsonSerializer.DeserializeAsync<GenreVM>(responseStream);
            }
            else
            {
                genre = new GenreVM();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GenreVM genre)
        {
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(new Uri("http://localhost:55169/api/Genres/" + id)).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }
    }
}