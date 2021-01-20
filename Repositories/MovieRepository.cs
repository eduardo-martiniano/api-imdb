using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_imdb.Contracts;
using api_imdb.Data;
using api_imdb.Models;
using api_imdb.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace api_imdb.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly Context _context;

        public MovieRepository(Context context)
        {
            _context = context;
        }
        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<List<Movie>> GetAll(MovieQuery query)
        {
            var moviesQuery = _context.Movies
                              .Include(m => m.Actings)
                              .ThenInclude(a => a.Actor)
                              .Include(m => m.Ratings)
                              .AsQueryable();

            if (!string.IsNullOrEmpty(query.DirectorName))
                moviesQuery =  moviesQuery.Where(m => m.DirectorName.ToLower().Contains(query.DirectorName.ToLower())).AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
                moviesQuery = moviesQuery.Where(m => m.Title.ToLower().Contains(query.Title.ToLower())).AsQueryable();

            if (!string.IsNullOrEmpty(query.Genre))
                moviesQuery = moviesQuery.Where(m => m.GenreName.ToLower().Contains(query.Genre.ToLower())).AsQueryable();

            return await moviesQuery
                            .OrderByDescending(m => m.Ratings.Count)
                            .ThenBy(m => m.Title)
                            .Skip(query.Offset * query.Limit)
                            .Take(query.Limit)
                            .ToListAsync();
        }

        public async Task<List<Movie>> GetByFilters(string title, string genre, Actor actor, int limit, int offset)
        {
            var query =  _context.Movies.Include(m => m.Actings).AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Include(m => m.Actings).Where(m => m.Title == title).AsQueryable();
            
            if (!string.IsNullOrEmpty(genre))
                query = query.Include(m => m.Actings).Where(m => m.GenreName == genre).AsQueryable();
            
            // if (actor != null)
            //     query = query.Include(m => m.Actings).Where(m => m.Actings.).AsQueryable();

            return await query.Skip((offset * limit)).Take(limit).ToListAsync();

        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies
                    .Include(m => m.Actings)
                    .ThenInclude(a => a.Actor)
                    .Include(m => m.Ratings)
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<Movie> Update(int id, Movie movie)
        {
            var _movie = await GetById(id);
            _movie = movie;
            _movie.Id = id;
            await _context.SaveChangesAsync();

            return _movie;
        }
    }
}