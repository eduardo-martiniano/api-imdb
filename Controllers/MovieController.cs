using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_imdb.Contracts;
using api_imdb.Models;
using api_imdb.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_imdb.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper, IMovieService movieService)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _movieService = movieService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var movies = _mapper.Map<IEnumerable<MovieViewModel>>(await _movieRepository.GetAll());
            return Ok(movies);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] MovieViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (model.Actors != null) await _movieService.CreateMovieWhithActor(model);

            return Ok(model);
        }


    }
}
