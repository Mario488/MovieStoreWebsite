using Microsoft.EntityFrameworkCore;
using move_store_app.Models;


namespace move_store_app.data
{
    public class MoviesRepo : IMovieRepo
    {
        public readonly MoviesDbContext context;

        public MoviesRepo(MoviesDbContext _context)
        {
            context = _context;
        }

        
        public void AddMovie(Movies movie)
        {
            context.Add(movie);
            context.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movieToDelete = context.Movies.Find(id);
            if(movieToDelete == null) { return; }
            context.Movies.Remove(movieToDelete);
            context.SaveChanges();
        }

        public bool GetMovieById(int id)
        {
            var movie = context.Movies.Find(id);
            return movie != null ? true : false;
        }

        public IEnumerable<Movies> GetMovies()
        {
            return (IEnumerable<Movies>)context.Movies.ToList();
        }

        public void UpdateMovie(Movies movie)
        {
            var movieToUpdate = context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (movieToUpdate == null)
            {
                return;
            }
            movieToUpdate.Title = movie.Title;
            movieToUpdate.Genre = movie.Genre;
            movieToUpdate.RelDate = movie.RelDate;
            movieToUpdate.Price = movie.Price;
            context.SaveChanges();
        }

        Movies IMovieRepo.GetMovieById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
