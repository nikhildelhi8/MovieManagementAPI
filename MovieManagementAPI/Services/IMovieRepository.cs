using Microsoft.AspNetCore.JsonPatch;
using MovieManagementAPI.Entities;
using MovieManagementAPI.FIlterClass;
using MovieManagementAPI.Models;
using System.Threading.Tasks;

namespace MovieManagementAPI.Services
{
    public interface IMovieRepository
    {

        Task<Movie?> GetMovieByIdAsync(int movieId);

        Task<IEnumerable<Movie>> GetAllMoviesAsync();


        Task<(IEnumerable<Movie>, PaginationMetaData)> SearchMoviesAsync(MovieFilterParameters filter, int pageNumber, int pageSize  );

        Task<Movie> AddMoviesAsync(CreatingNewMovieDTO newMovie);

        Task<Movie?> UpdateMovie(UpdateMovieDetailDTO newMovie, int id);

        void DeleteMovieByIdAsync(Movie movieDetails);

        Task<Movie?> GetCastDetails(int id); 

        Task<Cast?> PostCastDetails(int movieId , Cast castPost);

        Task<bool> SaveChangesAsync();




    }
}
