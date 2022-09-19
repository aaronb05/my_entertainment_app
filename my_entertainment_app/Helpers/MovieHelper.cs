using my_entertainment_app.Models;
using Newtonsoft.Json.Linq;

namespace my_entertainment_app.Helpers
{
    public class MovieHelper
    {
        public async Task<List<MovieNowPlaying>> GetNowPlaying(string apiKey)
        {
            List<MovieNowPlaying> movieData = new List<MovieNowPlaying>();
            //var apiKey = config["apiKey"];

            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/now_playing?api_key={apiKey}&language=en-US&page=1");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(apiResponse)["results"];

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

                movieData.Add(newMovie);
            }

            return movieData.ToList();
        }

        public async Task<JToken> GetMovieDetails(int movie_id, string apiKey)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movie_id}?api_key={apiKey}&language=en-US");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(apiResponse);

            Console.Write(data);

            return data;

        }

        public async Task<List<MovieDetails>> GetRelatedMovies(int movie_id, string apiKey)
        {
            
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movie_id}/similar?api_key={apiKey}&language=en-US&page=1");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(apiResponse)["results"];
            List<MovieDetails> relatedMovies = new List<MovieDetails>(); 
            List<Genre> genreList = new List<Genre>();

            if (data != null)
            {             
                foreach (var movie in data)
                {
                    var genres = movie["genre_ids"].ToList();
                    foreach (var item in genres)
                    {
                        var newGenre = new Genre()
                        {
                            Id = Convert.ToInt32(item),
                            Name = "N/A"
                        };
                        genreList.Add(newGenre);
                    }

                    var movieDetails = new MovieDetails()
                    {
                        Id = Convert.ToInt32(movie["id"].ToString()),
                        Adult = Convert.ToBoolean(movie["adult"]),
                        BackdropPath = movie["backdrop_path"].ToString(),
                        OriginalLanguage = movie["original_language"].ToString(),
                        OriginalTitle = movie["original_title"].ToString(),
                        Overview = movie["overview"].ToString(),
                        Popularity = (float)Convert.ToDouble(movie["popularity"].ToString()),
                        PosterPath = movie["poster_path"].ToString(),
                        ReleaseDate = Convert.ToDateTime(movie["release_date"]),
                        Title = movie["title"].ToString(),
                        VoteAverage = (float)Convert.ToDouble(movie["vote_average"].ToString()),
                        VoteCount = Convert.ToInt32(movie["vote_count"].ToString()),
                        Genres = genreList
                    };

                    if (movieDetails.PosterPath == "null")
                    {
                        movieDetails.PosterPath = "https://unsplash.it/200/500";
                    }

                    relatedMovies.Add(movieDetails);
                }

                return relatedMovies;
            }
            else
            {
                return relatedMovies;
            }
        }

    }
}
