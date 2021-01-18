using System;
using System.Collections.Generic;

namespace api_imdb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public ICollection<Actor> Actors { get; set; }
    }
}