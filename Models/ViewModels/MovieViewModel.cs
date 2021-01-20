using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O nome do filme deve ter no maximo 50 caracteres!")]
        [DisplayName("Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo Diretor é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo Diretor deve ter no maximo 50 caracteres!")]
        [DisplayName("Diretor")]
        public string DirectorName { get; set; }

        [Required(ErrorMessage = "O campo Genero é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O nome do Genero deve ter no maximo 15 caracteres!")]
        [DisplayName("Genero")]
        public string GenreName { get; set; }
        public List<ActorViewModel> Actors { get; set; }
        
        public MovieViewModel()
        {
            Actors = new List<ActorViewModel>();
        }
    }
}
