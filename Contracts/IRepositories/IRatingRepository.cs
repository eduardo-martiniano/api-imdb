using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Contracts.IRepositories
{
    public interface IRatingRepository
    {
        Task Add(int movieId, int note);
    }
}
