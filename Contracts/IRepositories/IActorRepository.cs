using api_imdb.Models;
using System.Threading.Tasks;

namespace api_imdb.Contracts.IRepositories
{
    public interface IActorRepository
    {
        Task<Actor> Add(Actor actor);
    }
}
