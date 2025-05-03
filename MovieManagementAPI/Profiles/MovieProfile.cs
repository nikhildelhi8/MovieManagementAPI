using AutoMapper;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Profiles
{
    public class MovieProfile : Profile
    {


        public MovieProfile()
        {

            CreateMap<Entities.Movie, Models.MovieDTO>()
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name))
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => new MovieDetailsResponseDTO
                {
                    Genre = src.Detail.Genre,
                    ReleaseDate = src.Detail.ReleaseDate
                }))
                .ForMember(dest => dest.CastNames , opt => opt.MapFrom(src => src.movieCast.Select(mc => mc.Cast.Name).ToList()));


            CreateMap<Models.CreatingNewMovieDTO, Entities.Movie>()
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => new Entities.MovieDetails
                {
                    Genre = src.MovieDetails.Genre,
                    ReleaseDate = src.MovieDetails.ReleaseDate
                }))
                .ForMember(dest => dest.DirectorId, opt => opt.Ignore())
                .ForMember(dest => dest.movieCast, opt => opt.Ignore());


            CreateMap<Models.UpdateMovieDetailDTO, Entities.Movie>()
                .ForMember(dest => dest.DirectorId, opt => opt.Ignore())
                .ForMember(dest => dest.movieCast, opt => opt.Ignore())
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => new Entities.MovieDetails
                {
                    Genre = src.MovieDetails.Genre,
                    ReleaseDate = src.MovieDetails.ReleaseDate
                }));

            CreateMap<Models.UpdatePartialMovieUpdates, Entities.Movie>()
                .ForMember(dest => dest.DirectorId, opt => opt.Ignore())
                .ForMember(dest => dest.movieCast, opt => opt.Ignore())
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => new Entities.MovieDetails
                {
                    Genre = src.MovieDetails.Genre,
                    ReleaseDate = src.MovieDetails.ReleaseDate
                }));


            CreateMap<Movie, MovieUpdateDto>().ReverseMap();
            CreateMap<MovieDetails, MovieDetailsUpdateDto>().ReverseMap();
            CreateMap<Director, DirectorUpdateDto>().ReverseMap();



        }
    }
}
