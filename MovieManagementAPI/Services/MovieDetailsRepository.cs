using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;
using System.Threading.Tasks;

namespace MovieManagementAPI.Services
{
    public class MovieDetailsRepository : IMovieDetailsRepository
    {
        private readonly MovieDbContext _movieDbContext;

        private readonly IMapper _mapper; 


        public MovieDetailsRepository(MovieDbContext movieDbContext , IMapper mapper)
        {
            _movieDbContext = movieDbContext ?? throw new ArgumentNullException(nameof(movieDbContext));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MovieDetails> AddMovieDetailsAsync(int movieId, MovieDetailDTO movieDetails)
        {

            var newMovieDetail = new MovieDetails
            {
                Genre = movieDetails.Genre,
                MovieId = movieId,
                ReleaseDate = movieDetails.ReleaseDate,
            };


            await _movieDbContext.MovieDetails.AddAsync(newMovieDetail);

            await _movieDbContext.SaveChangesAsync();


            return newMovieDetail; 

        }

        public async Task<MovieDetails?> MovieDetailByIdAsync ( int id)
        {
            try
            {
                var movieDetailsById = await _movieDbContext.MovieDetails.FirstOrDefaultAsync(md => md.MovieId == id);
                return movieDetailsById;

            }

            catch(Exception e)
            {
                throw new Exception("An error occured while retrieving the movie");
            }

        }


        public async void DeleteMovieById(MovieDetails movieEntity)
        {
          _movieDbContext.MovieDetails.Remove(movieEntity);
           
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _movieDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);
            }
        }

        public async Task<MovieDetails> UpdateMovieDetails(MovieDetailsUpdateDto newMovieDetailDTO, MovieDetails existingMovieDetailEntity)
        {

            try
            {

                existingMovieDetailEntity.ReleaseDate = newMovieDetailDTO.ReleaseDate;
                existingMovieDetailEntity.Genre = newMovieDetailDTO.Genre;

                return existingMovieDetailEntity;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while updating the movie ");
            }

        }
    }
}
