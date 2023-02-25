using Microsoft.EntityFrameworkCore;
using move_store_app.Models;

namespace move_store_app.data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {}
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MoviesCart> Cart { get; set; }
        public DbSet<Registration_Login> Reg_Log { get; set; }
        public DbSet<IsLoggedIn> IsLogged { get; set; }

    }
}
