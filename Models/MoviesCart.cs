using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace move_store_app.Models
{
    public class MoviesCart
    {
        public Guid Id { get; set; }
        public List<Movies> movies = new List<Movies>();
        public void AddMovie(Movies movie)
        {
            movies.Add(movie);
        }
        public void DeleteMovie(string Title)
        {
            Movies movieToDel = new Movies();
            foreach (var movie in movies)
            {
                if(movie.Title == Title)
                {
                    movieToDel = movie;
                }
            }
            movies.Remove(movieToDel);
        }
    }
}
