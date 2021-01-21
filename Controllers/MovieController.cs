using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_imdb.Configuration;
using api_imdb.Contracts;
using api_imdb.Models;
using api_imdb.Models.Jsons;
using api_imdb.Models.Queries;
using api_imdb.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_imdb.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] MovieQuery query)
        {
            var movies = await _movieRepository.GetAll(query);
            var moviesJson = movies.Select(x => new MovieJson(x)).ToList();
            return Ok(moviesJson);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] int id )
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound("Filme não encontrado!");

            return new MovieJson(movie);
        }

        [HttpPost]
        [Route("")]
        [ClaimsAuthorize("ADM", "Add")]
        public async Task<IActionResult> Add([FromBody] MovieViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos!");

            var movie = await _movieService.CreateMovie(model);
            var movieJson = new MovieJson(movie);

            return StatusCode(201, movieJson);
        }

        [HttpPost]
        [Route("rate-movie")]
        [ClaimsAuthorize("USER", "Rating")]
        public async Task<IActionResult> RateMovie([FromQuery] int movieId, [FromQuery] int note)
        {
            if (note < 0 || note > 4) return BadRequest("Nota invalida! Digite uma nota entre 0-4");

            var movie = await _movieRepository.GetById(movieId);

            if (movie == null) return BadRequest("Filme não encontrado");

            await _movieService.RateMovie(movieId, note);

            return StatusCode(202, new MovieJson(movie));
        }
    }
}
