using System.Collections.Generic;
using System.Threading.Tasks;
using api_imdb.Models;

namespace api_imdb.Contracts
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAll(int limit, int offset);
        Task<Movie> GetById(int id);
        Task<List<Movie>> GetByFilters(string name, string genre, Actor actor, int limit, int offset);
        Task<Movie> Add(Movie movie);
        Task<Movie> Update(int id, Movie movie);
    }
}