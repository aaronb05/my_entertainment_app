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
        private readonly TvShowHelper tvShowHelper;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, MovieHelper movieHelper, TvShowHelper tvShowHelper)
        {
            _logger = logger;
            this.config = config;            
            this.movieHelper = movieHelper;
            this.tvShowHelper = tvShowHelper; 
        }


        public async Task<IActionResult> Index()
        {
            var nowPlaying = await movieHelper.GetNowPlaying();

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

        public IActionResult MoviesIndex()
        {

            return View();
        }

        public async Task<IActionResult> MoviesDetails(int movie_id)
        {           
            //var apiKey = config["apiKey"];

            var movieDetails = await movieHelper.GetMovieDetails(movie_id); 
            var relatedMovies = await movieHelper.GetRelatedMovies(movie_id);

            MovieDetailsVM movieDetailsVM = new MovieDetailsVM()
            {
                Movie = movieDetails,
                RelatedMovies = relatedMovies.Skip(1).Take(4).ToList()
            };


            return View(movieDetailsVM);      
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