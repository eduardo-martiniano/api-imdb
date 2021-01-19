using System.Collections.Generic;

namespace api_imdb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DirectorName { get; set; }
        public string GenreName { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Acting> Actings { get; set; }
    }
}