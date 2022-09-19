using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using my_entertainment_app.Helpers;
using my_entertainment_app.Models;
using my_entertainment_app.ViewModels;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace my_entertainment_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration config;
        private readonly MovieHelper movieHelper;            

        public HomeController(ILogger<HomeController> logger, IConfiguration config, MovieHelper movieHelper)
        {
            _logger = logger;
            this.config = config;            
            this.movieHelper = movieHelper;
        }


        public async Task<IActionResult> Index()
        {
            List<MovieNowPlaying> nowPlaying = new List<MovieNowPlaying>();
            var apiKey = config["apiKey"];

            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}&language=en-US&page=1");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(apiResponse)["results"];

            if (data != null)
            {
                foreach (var movie in data)
                {
                    var newMovie = new MovieNowPlaying()
                    {
                        Id = Convert.ToInt32(movie["id"].ToString()),
                        OriginalTitle = movie["original_title"].ToString(),
                        OriginalLanguage = movie["original_language"].ToString(),
                        Overview = movie["overview"].ToString(),
                        Popularity = (float)Convert.ToDouble(movie["popularity"].ToString()),
                        PosterPath = movie["poster_path"].ToString(),
                        BackdropPath = movie["backdrop_path"].ToString(),
                        ReleaseDate = Convert.ToDateTime(movie["release_date"].ToString()),
                        Title = movie["title"].ToString(),
                        Video = Convert.ToBoolean(movie["video"].ToString()),
                        VoteAverage = (float)Convert.ToDouble(movie["vote_average"]),
                        VoteCount = Convert.ToInt32(movie["vote_average"])
                    };

                    nowPlaying.Add(newMovie);
                }

                var dashboardVM = new DashboardVM()
                {
                    MovieOne = nowPlaying.FirstOrDefault(),
                    MovieTwo = nowPlaying.Skip(1).FirstOrDefault(),
                    MovieThree = nowPlaying.Skip(2).FirstOrDefault(),
                    MovieFour = nowPlaying.Skip(3).FirstOrDefault(),
                    MovieFive = nowPlaying.Skip(4).FirstOrDefault(),
                    Movies = nowPlaying.Take(10).ToList()
                };

                return View(dashboardVM);
            }
            else
            {
                return View();

            }
        }

        public IActionResult MoviesIndex()
        {

            return View();
        }

        public async Task<IActionResult> MoviesDetails(int movie_id)
        {
           
            var apiKey = config["apiKey"];
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movie_id}?api_key={apiKey}&language=en-US");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var movie = JObject.Parse(apiResponse);
            List<Genre> genreList = new List<Genre>();

            if (movie != null)
            {
                var genres = movie["genres"];

                foreach (var item in genres)
                {
                    var newGenre = new Genre()
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Name = item["name"].ToString()
                    };
                    genreList.Add(newGenre);
                }

                var movieDetails = new MovieDetails()
                {
                    Id = movie_id,
                    Adult = Convert.ToBoolean(movie["adult"]),
                    BackdropPath = movie["backdrop_path"].ToString(),
                    Budget = Convert.ToInt32(movie["budget"]),
                    Homepage = movie["homepage"].ToString(),
                    OriginalLanguage = movie["original_language"].ToString(),
                    OriginalTitle = movie["original_title"].ToString(),
                    Overview = movie["overview"].ToString(),
                    Popularity = (float)Convert.ToDouble(movie["popularity"].ToString()),
                    PosterPath = movie["poster_path"].ToString(),
                    ReleaseDate = Convert.ToDateTime(movie["release_date"]),
                    Revenue = Convert.ToInt32(movie["revenue"]),
                    Runtime = Convert.ToInt32(movie["runtime"]),
                    Status = movie["status"].ToString(),
                    Tagline = movie["tagline"].ToString(),
                    Title = movie["title"].ToString(),
                    VoteAverage = (float)Convert.ToDouble(movie["vote_average"].ToString()),
                    VoteCount = Convert.ToInt32(movie["vote_count"].ToString()),
                    Genres = genreList
                };

                //var relatedMovies = new List<MovieDetails>();   
                var relatedMovies = await movieHelper.GetRelatedMovies(movie_id, apiKey);

                MovieDetailsVM movieDetailsVM = new MovieDetailsVM()
                {
                    Movie = movieDetails,
                    RelatedMovies = relatedMovies.Skip(1).Take(4).ToList()
                };


                return View(movieDetailsVM);
            }

            return View();
        }       

        public IActionResult TvIndex()
        {

            return View();
        }

        public IActionResult TvDetails(int id)
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