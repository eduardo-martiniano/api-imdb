using api_imdb.Contracts.IRepositories;
using api_imdb.Data;
using api_imdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly Context _context;

        public ActorRepository(Context context)
        {
            _context = context;
        }

        public async Task<Actor> Add(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();

            return actor;
        }
    }
}
