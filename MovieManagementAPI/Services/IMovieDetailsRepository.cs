using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;
using System.Threading.Tasks;

namespace MovieManagementAPI.Services
{
    public interface IMovieDetailsRepository
    {
        Task<MovieDetails?> MovieDetailByIdAsync(int id);

        Task<MovieDetails> AddMovieDetailsAsync(int movieId , MovieDetailDTO movieDetails);

        void DeleteMovieById(MovieDetails movieEntity);

        Task<MovieDetails> UpdateMovieDetails(MovieDetailsUpdateDto newMovieDetailDTO, MovieDetails existingMovieDetailEntity);

        Task<bool> SaveChangesAsync();


    }
}
