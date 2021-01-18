using api_imdb.Models;
using Microsoft.EntityFrameworkCore;

namespace api_imdb.Data
{
    public class Context : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}