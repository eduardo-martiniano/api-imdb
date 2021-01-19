using api_imdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Contracts.IRepositories
{
    public interface IActingRepository
    {
        Task AddRelation(Acting acting);
    }
}
