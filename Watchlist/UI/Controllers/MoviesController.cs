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
using UI.Models.Genre;
using UI.Models.Movie;

namespace UI.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MoviesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<MovieGetVM> movies;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Movies");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                movies = await JsonSerializer.DeserializeAsync<IEnumerable<MovieGetVM>>(responseStream);
            }
            else
            {
                movies = Array.Empty<MovieGetVM>();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Name.Contains(searchString)
                                       || m.Director.Contains(searchString));
            }

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            MovieVM movie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Movies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                movie = await JsonSerializer.DeserializeAsync<MovieVM>(responseStream);
            }
            else
            {
                movie = new MovieVM();
            }

            return View(movie);
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
        public async Task<IActionResult> Create(MoviePostVM moviePost)
        {
            if (moviePost.MovieGenreDTOs == null)
            {
                moviePost.MovieGenreDTOs = new MovieGenreVM[0];
            }
            if (moviePost.MovieActorDTOs == null)
            {
                moviePost.MovieActorDTOs = new MovieActorVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var movieContent = new StringContent(JsonSerializer.Serialize(moviePost), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(new Uri("http://localhost:55169/api/Movies"), movieContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(moviePost);
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

            MovieVM movie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Movies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                movie = await JsonSerializer.DeserializeAsync<MovieVM>(responseStream);
            }
            else
            {
                movie = new MovieVM();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieVM movie)
        {
            if (movie.MovieGenreDTOs == null)
            {
                movie.MovieGenreDTOs = new MovieGenreVM[0];
            }
            if (movie.MovieActorDTOs == null)
            {
                movie.MovieActorDTOs = new MovieActorVM[0];
            }
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var movieContent = new StringContent(JsonSerializer.Serialize(movie), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Movies/" + id), movieContent).ConfigureAwait(false);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MovieDeleteVM movie;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Movies/" + id);
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                movie = await JsonSerializer.DeserializeAsync<MovieDeleteVM>(responseStream);
            }
            else
            {
                movie = new MovieDeleteVM();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, MovieDeleteVM movieDelete)
        {
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(new Uri("http://localhost:55169/api/Movies/" + id)).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(movieDelete);
        }
    }
}