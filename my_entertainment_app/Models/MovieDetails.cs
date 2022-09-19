using my_entertainment_app.Helpers;

namespace my_entertainment_app.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }

        public bool Adult { get; set; }

        public string BackdropPath { get; set; }

        public int? Budget { get; set; }

        public string? Homepage { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public float Popularity { get; set; }

        public object PosterPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int? Revenue { get; set; }

        public int? Runtime { get; set; }

        public string? Status { get; set; }

        public string? Tagline { get; set; }

        public string Title { get; set; }

        public float VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public List<Genre> Genres {get; set;}
    }
}
