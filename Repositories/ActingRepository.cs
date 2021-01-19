using api_imdb.Contracts.IRepositories;
using api_imdb.Data;
using api_imdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Repositories
{
    public class ActingRepository : IActingRepository
    {
        private readonly Context _context;

        public ActingRepository(Context context)
        {
            _context = context;
        }

        public async Task AddRelation(Acting acting)
        {
            await _context.Actings.AddAsync(acting);
            await _context.SaveChangesAsync();
        }
    }
}
