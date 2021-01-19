using System.ComponentModel.DataAnnotations;

namespace api_imdb.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Note { get; set; }
    }
}