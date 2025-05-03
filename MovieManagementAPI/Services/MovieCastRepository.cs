using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Services
{
    public class MovieCastRepository : IMovieCastRepository
    {

        private readonly MovieDbContext _movieContext; 


        public MovieCastRepository(MovieDbContext movieDbContext)
        {
            _movieContext = movieDbContext ?? throw new ArgumentNullException(nameof(movieDbContext));
        }


        public async Task<Cast?> GetCastDetailsByIdAsync(int id)
        {

            try
            {
                var castDetails = await _movieContext.Casts.Include(c => c.MovieCast).ThenInclude(mc => mc.Movie).FirstOrDefaultAsync(c => c.Id == id);

                if (castDetails == null)
                    return null;

                return castDetails;
            }

            catch (Exception ex)
            {
                throw new Exception("some error occured while fetching cast details");
            }

        }


        public async Task<Cast?> UpdateCastDetailsByIdAsync(int id, CastUpdateDTO castUpdateDetails)
        {
            try
            {
                var castDetailsEntity = await GetCastDetailsByIdAsync(id);

                if (castDetailsEntity == null) return null; 

                

                castDetailsEntity.Name = castUpdateDetails.Name;

                return castDetailsEntity;




            }

            catch(Exception ex)
            {
                throw new Exception("Some error occured while updating data");

            }
        }


        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _movieContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);
            }
        }

        public void DeleteCastDetailsByIdAsync(int id , Cast castDetails)
        {

            _movieContext.Remove(castDetails);
        }
    }
}
