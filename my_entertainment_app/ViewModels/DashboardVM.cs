using my_entertainment_app.Models;

namespace my_entertainment_app.ViewModels
{
    public class DashboardVM
    {
        public MovieNowPlaying MovieOne { get; set; }
        public MovieNowPlaying MovieTwo { get; set; }
        public MovieNowPlaying MovieThree { get; set; }
        public MovieNowPlaying MovieFour { get; set; }
        public MovieNowPlaying MovieFive { get; set; }


        public List<MovieNowPlaying> Movies { get; set; }
    }
}
