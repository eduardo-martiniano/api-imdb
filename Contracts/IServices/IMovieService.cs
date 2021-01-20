using api_imdb.Models;
using api_imdb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Contracts
{
    public interface IMovieService
    {
        Task<Movie> CreateMovie(MovieViewModel model);
        Task RateMovie(int movieId, int note);
    }
}
