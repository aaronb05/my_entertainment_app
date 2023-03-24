using my_entertainment_app.Models;

namespace my_entertainment_app.ViewModels
{
    public class MovieIndexVM
    {
        public List<MovieDetails> TopRated { get; set; }
        
        public List<MovieDetails> Popular { get; set; }

        public List<MovieDetails> Upcoming { get; set; }
    }
}
