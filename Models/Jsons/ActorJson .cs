using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.Jsons
{
    public class ActorJson : IActionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ActorJson(Actor actor)
        {
            Id = actor.Id;
            Name = actor.Name;    
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
