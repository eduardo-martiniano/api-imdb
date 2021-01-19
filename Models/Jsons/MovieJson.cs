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

        public MovieJson(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            DirectorName = movie.DirectorName;
            GenreName = movie.GenreName;
            Actors = movie.Actings.Select(a => new ActorJson(a.Actor)).ToList();
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
