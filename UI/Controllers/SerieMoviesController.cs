using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class SerieMoviesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SerieMoviesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SerieMovieGetVM> serieMovies;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/SerieMovies");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                serieMovies = await JsonSerializer.DeserializeAsync<IEnumerable<SerieMovieGetVM>>(responseStream);
            }
            else
            {
                serieMovies = Array.Empty<SerieMovieGetVM>();
            }

            return View(serieMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            SerieMovieVM serieMovie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/SerieMovies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                serieMovie = await JsonSerializer.DeserializeAsync<SerieMovieVM>(responseStream);
            }
            else
            {
                serieMovie = new SerieMovieVM();
            }

            return View(serieMovie);
        }

        public async Task<IActionResult> Create()
        {
            if (!HttpContext.Session.Keys.Contains("actors"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors");
                request.Headers.Add("Accept", "application/json");

                var client = _httpClientFactory.CreateClient();

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("actors", responseString);
                }
            }

            if (!HttpContext.Session.Keys.Contains("genres"))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres");
                request.Headers.Add("Accept", "application/json");

                var client = _httpClientFactory.CreateClient();

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("genres", responseString);
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SerieMoviePostVM serieMoviePost)
        {
            if (serieMoviePost.SerieMovieGenreDTOs == null)
            {
                serieMoviePost.SerieMovieGenreDTOs = new SerieMovieGenreVM[0];
            }
            if (serieMoviePost.SerieMovieActorDTOs == null)
            {
                serieMoviePost.SerieMovieActorDTOs = new SerieMovieActorVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var serieMovieContent = new StringContent(JsonSerializer.Serialize(serieMoviePost), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri("http://localhost:55169/api/SerieMovies"), serieMovieContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(serieMoviePost);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!HttpContext.Session.Keys.Contains("actors"))
            {
                using var actorsRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Actors");
                actorsRequest.Headers.Add("Accept", "application/json");

                var actorsClient = _httpClientFactory.CreateClient();

                var actorsResponse = await actorsClient.SendAsync(actorsRequest).ConfigureAwait(false);

                if (actorsResponse.IsSuccessStatusCode)
                {
                    string responseString = await actorsResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("actors", responseString);
                }
            }

            if (!HttpContext.Session.Keys.Contains("genres"))
            {
                using var genresRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Genres");
                genresRequest.Headers.Add("Accept", "application/json");

                var genresClient = _httpClientFactory.CreateClient();

                var genresResponse = await genresClient.SendAsync(genresRequest).ConfigureAwait(false);

                if (genresResponse.IsSuccessStatusCode)
                {
                    string responseString = await genresResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.SetString("genres", responseString);
                }
            }

            SerieMovieVM serieMovie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/SerieMovies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                serieMovie = await JsonSerializer.DeserializeAsync<SerieMovieVM>(responseStream);
            }
            else
            {
                serieMovie = new SerieMovieVM();
            }

            return View(serieMovie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SerieMovieVM serieMovie)
        {
            if (serieMovie.SerieMovieGenreDTOs == null)
            {
                serieMovie.SerieMovieGenreDTOs = new SerieMovieGenreVM[0];
            }
            if (serieMovie.SerieMovieActorDTOs == null)
            {
                serieMovie.SerieMovieActorDTOs = new SerieMovieActorVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var serieMovieContent = new StringContent(JsonSerializer.Serialize(serieMovie), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/SerieMovies/" + id), serieMovieContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(serieMovie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            SerieMovieDeleteVM serieMovie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/SerieMovies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                serieMovie = await JsonSerializer.DeserializeAsync<SerieMovieDeleteVM>(responseStream);
            }
            else
            {
                serieMovie = new SerieMovieDeleteVM();
            }

            return View(serieMovie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, SerieMovieDeleteVM serieMovieDelete)
        {
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(new Uri("http://localhost:55169/api/SerieMovies/" + id)).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(serieMovieDelete);
        }
    }
}