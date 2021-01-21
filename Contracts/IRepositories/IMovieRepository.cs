using System.Collections.Generic;
using System.Threading.Tasks;
using api_imdb.Models;
using api_imdb.Models.Queries;

namespace api_imdb.Contracts
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAll(MovieQuery query);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
    }
}