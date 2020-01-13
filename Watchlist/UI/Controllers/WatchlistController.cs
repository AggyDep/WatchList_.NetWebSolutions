using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Extensions;
using UI.Models.Movie;
using UI.Models.Watchlist;

namespace UI.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WatchlistController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index(string id)
        {
            IEnumerable<WatchlistVM> items;
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + id + "/watchlist");
            request.Headers.Add("Accept", "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                items = await JsonSerializer.DeserializeAsync<IEnumerable<WatchlistVM>>(responseStream);
            }
            else
            {
                items = Array.Empty<WatchlistVM>();
            }

            List<WatchlistShowVM> movies = new List<WatchlistShowVM>();
            foreach (WatchlistVM watchListItem in items)
            {
                MovieVM movie = await getMovie(watchListItem.MovieId);
                WatchlistShowVM item = new WatchlistShowVM()
                {
                    UserId = watchListItem.UserId,
                    MovieId = watchListItem.MovieId,
                    MovieName = movie.Name,
                    Image = movie.Image,
                    Status = watchListItem.Status,
                    Score = watchListItem.Score
                };
                movies.Add(item);
            }

            return View(movies);
        }

        public async Task<MovieVM> getMovie(int id)
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

            return movie;
        }
    }
}