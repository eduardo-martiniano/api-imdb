using api_imdb.Models;
using api_imdb.Models.ViewModels;
using AutoMapper;

namespace api_imdb.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Acting, ActingViewModel>().ReverseMap();
            CreateMap<Actor, ActorViewModel>().ReverseMap();
            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Rating, RatingViewModel>().ReverseMap();
        }
    }
}
