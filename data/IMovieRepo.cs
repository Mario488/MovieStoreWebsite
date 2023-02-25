using move_store_app.Models;
namespace move_store_app.data
{
    public interface IMovieRepo
    {
        IEnumerable<Movies> GetMovies();
        Movies GetMovieById(int id);
        void AddMovie(Movies movie);
        void UpdateMovie(Movies movie);
        void DeleteMovie(int id);
    }
}
