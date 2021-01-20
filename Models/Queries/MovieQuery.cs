using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.Queries
{
    public class MovieQuery
    {
        public string DirectorName { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }

        public MovieQuery()
        {
            Limit = 30;
            Offset = 0;
        }
    }
}
