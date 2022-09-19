using my_entertainment_app.Models;

namespace my_entertainment_app.ViewModels
{
    public class MovieDetailsVM
    {
        public MovieDetails Movie { get; set; }

        public List<MovieDetails> RelatedMovies { get; set; }


    }
}
