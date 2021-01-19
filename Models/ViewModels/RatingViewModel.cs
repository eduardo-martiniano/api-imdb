using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }
        public int Note { get; set; }
    }
}
