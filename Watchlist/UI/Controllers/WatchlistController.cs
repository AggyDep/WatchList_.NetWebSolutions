using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Extensions;
using UI.Models.Movie;
using UI.Models.User;
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

        public async Task<IActionResult> Add(string userId, string movieId)
        {
            WatchlistShowVM item;
            MovieVM movie = await getMovie(Int32.Parse(movieId));

            item = new WatchlistShowVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId),
                MovieName = movie.Name,
                Image = movie.Image,
                Status = "",
                Score = 0
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string userId, string movieId, WatchlistShowVM watchlistShow)
        {
            WatchlistVM watchlistItem = new WatchlistVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId),
                Score = 0,
                Status = "PlanToWatch"
            };
            UserVM currentUser = await getCurrentUser(userId);
            UserPutVM userPut = new UserPutVM() {
                Id = userId,
                Username = currentUser.Username,
                Name = currentUser.Name,
                LastName = currentUser.LastName,
                Email = currentUser.Email,
                Birthday = currentUser.Birthday,
                About = currentUser.About,
                Image = currentUser.Image,
                WatchListDTOs = currentUser.WatchListDTOs
            };

            userPut.WatchListDTOs.Add(watchlistItem);

            var client = _httpClientFactory.CreateClient();
            var userContent = new StringContent(JsonSerializer.Serialize(userPut), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Users/" + userId), userContent).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContext.Session.Remove("watchlist");
                HttpContext.Session.SetString("watchlist", JsonSerializer.Serialize(userPut.WatchListDTOs));
                return RedirectToAction(nameof(Index), new { id = userId });
            }
            return RedirectToAction(nameof(Index), new { id = userId });
        }

        public async Task<IActionResult> Update(string userId, string movieId)
        {
            WatchlistShowVM item;
            WatchlistVM watchlistItem = new WatchlistVM();
            MovieVM movie = await getMovie(Int32.Parse(movieId));

            IEnumerable<WatchlistVM> items;
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + userId + "/watchlist");
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

            foreach (WatchlistVM watchListItem in items)
            {
                if (watchListItem.UserId == userId && watchListItem.MovieId == Int32.Parse(movieId))
                {
                    watchlistItem = watchListItem;
                }
            }

            item = new WatchlistShowVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId),
                MovieName = movie.Name,
                Image = movie.Image,
                Status = watchlistItem.Status,
                Score = watchlistItem.Score
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string userId, string movieId, WatchlistShowVM watchlistShow)
        {
            WatchlistVM watchlistUpdate = new WatchlistVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId),
                Status = watchlistShow.Status,
                Score = watchlistShow.Score

            };
            var client = _httpClientFactory.CreateClient();
            var itemContent = new StringContent(JsonSerializer.Serialize(watchlistUpdate), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await client.PutAsync(new Uri("http://localhost:55169/api/Users/" + userId + "/watchlist"), itemContent).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), new { id = userId });
            }
            return RedirectToAction(nameof(Index), new { id = userId });
        }

        public async Task<IActionResult> Delete(string userId, string movieId)
        {
            WatchlistShowVM item;
            WatchlistVM watchlistItem = new WatchlistVM();
            MovieVM movie = await getMovie(Int32.Parse(movieId));

            IEnumerable<WatchlistVM> items;
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + userId + "/watchlist");
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

            foreach (WatchlistVM watchListItem in items)
            {
                if (watchListItem.UserId == userId && watchListItem.MovieId == Int32.Parse(movieId))
                {
                    watchlistItem = watchListItem;
                }
            }

            item = new WatchlistShowVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId),
                MovieName = movie.Name,
                Image = movie.Image,
                Status = watchlistItem.Status,
                Score = watchlistItem.Score
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string userId, string movieId, WatchlistShowVM watchlistShow)
        {
            WatchlistPostDeleteVM watchlistDelete = new WatchlistPostDeleteVM()
            {
                UserId = userId,
                MovieId = Int32.Parse(movieId)
            };

            var client = _httpClientFactory.CreateClient();
            var reqeust = new HttpRequestMessage 
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost:55169/api/Users/" + userId + "/watchlist"),
                Content = new StringContent(JsonSerializer.Serialize(watchlistDelete), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(reqeust);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var watchListRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55169/api/Users/" + userId + "/watchlist");
                watchListRequest.Headers.Add("Accept", "application/json");

                var watchListClient = _httpClientFactory.CreateClient();
                var response = await watchListClient.SendAsync(watchListRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HttpContext.Session.Remove("watchlist");
                    HttpContext.Session.SetString("watchlist", responseString);
                }
                TempData["FilmId"] = null;
                return RedirectToAction(nameof(Index), new { id = userId });
            }
            TempData["FilmId"] = null;
            return RedirectToAction(nameof(Index), new { id = userId });
        }

        private async Task<MovieVM> getMovie(int id)
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

        private async Task<UserVM> getCurrentUser(string id)
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
            return user;
        }
    }
}