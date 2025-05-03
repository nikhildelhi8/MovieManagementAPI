using AutoMapper;

namespace MovieManagementAPI.Profiles
{
    public class MovieCastProfile : Profile
    {
        public MovieCastProfile()
        {
            CreateMap<Entities.Cast, Models.CastNameDTO>().ReverseMap();
            CreateMap<Models.CastPostDTO, Entities.Cast>();

            CreateMap<Entities.Cast, Models.CastDTO>()
                .ForMember(dest => dest.movies,
                    opt => opt.MapFrom(src => src.MovieCast
                        .Where(mc => mc.Movie != null)
                        .Select(mc => mc.Movie.Title)
                        .ToList()));


            CreateMap<Entities.Cast, Models.CastUpdateDTO>();
        }
    }

}
