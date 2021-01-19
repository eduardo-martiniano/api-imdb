using api_imdb.Contracts;
using api_imdb.Contracts.IRepositories;
using api_imdb.Models;
using api_imdb.Models.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IActingRepository _actingRepository;
        private readonly IMapper _mapper; 

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IMapper mapper, IActingRepository actingRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
            _actingRepository = actingRepository;
        }

        public async Task<MovieViewModel> CreateMovieWhithActor(MovieViewModel model)
        {
            var movie = await _movieRepository.Add(_mapper.Map<Movie>(model));
            
            foreach (var actor in model.Actors)
            {
                var _actor = await _actorRepository.Add(_mapper.Map<Actor>(actor));
                await _actingRepository.AddRelation(new Acting {MovieId = movie.Id, ActorId = _actor.Id });
            }

            return  _mapper.Map<MovieViewModel>(movie);
        }
    }
}
