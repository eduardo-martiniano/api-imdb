using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.Jsons
{
    public class MovieJson : IActionResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DirectorName { get; set; }
        public string GenreName { get; set; }
        public List<ActorJson> Actors { get; set; }
        public int CountRates { get; set; }
        public float Rating { get; set; }

        public MovieJson(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            DirectorName = movie.DirectorName;
            GenreName = movie.GenreName;
            
            if (movie.Actings != null)
                Actors = movie.Actings.Select(a => new ActorJson(a.Actor)).ToList();

            if (movie.Ratings != null && movie.Ratings.Any())
            {
                CountRates = movie.Ratings.Count();
                Rating = (float) (movie.Ratings.Select(r => r.Note).Sum()) / CountRates;
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
