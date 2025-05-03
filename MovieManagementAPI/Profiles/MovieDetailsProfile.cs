using AutoMapper;

namespace MovieManagementAPI.Profiles
{
    public class MovieDetailsProfile : Profile
    {

        public MovieDetailsProfile()
        {
            CreateMap<Entities.MovieDetails, Models.MovieDetailDTO>().ReverseMap();
        }
    }
}
