using System.Collections.Generic;

namespace api_imdb.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}