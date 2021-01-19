using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_imdb.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Acting> Actings { get; set; }
    }
}