using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.ViewModels
{
    public class ActingViewModel
    {
        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }
        public int ActorId { get; set; }
        public ActorViewModel Actor { get; set; }
    }
}
