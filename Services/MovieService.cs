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
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper; 

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IMapper mapper, IActingRepository actingRepository, IRatingRepository ratingRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
            _actingRepository = actingRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<Movie> CreateMovie(MovieViewModel model)
        {
            var movie = await _movieRepository.Add(_mapper.Map<Movie>(model));

            if (!model.Actors.Any()) return movie;

            foreach (var actor in model.Actors)
            {
                var _actor = await CreateActor(actor);
                await CreateRelationMovieAndActor(movie.Id, _actor.Id);
            }

            return movie;
        }

        public async Task RateMovie(int movieId, int note)
        {
            await _ratingRepository.Add(movieId, note);
        }

        private async Task<ActorViewModel> CreateActor(ActorViewModel model)
        {
            var actor = await _actorRepository.Add(_mapper.Map<Actor>(model));

            return _mapper.Map<ActorViewModel>(actor);
        }

        private async Task CreateRelationMovieAndActor(int movieId, int actorId)
        {
            await _actingRepository.AddRelation(new Acting { MovieId = movieId, ActorId = actorId });
        }
    }
}
