using AutoMapper;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Profiles
{
    public class DirectorProfile : Profile
    {

        public DirectorProfile() {

            //CreateMap<Director, DirectorDTO>()
            //                .ForMember(dest => dest.movies,
            //                           opt => opt.MapFrom(src => src.Movies.Select(m => m.Title).ToList()));


            CreateMap<Director, DirectorDTO>()
                    .ForMember(dest => dest.movies, opt => opt.Ignore());

        }


    }
}
