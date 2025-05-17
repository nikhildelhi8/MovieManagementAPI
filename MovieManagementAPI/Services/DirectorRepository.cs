using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Services
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieDbContext _movieContext;



        public DirectorRepository(MovieDbContext movieContext)
        {
            _movieContext = movieContext ?? throw new ArgumentNullException(nameof(movieContext));
        }


        public async Task<IEnumerable<Director>> GetAllDirectorDetailsAsync()
        {

            var directorDetails = await _movieContext.Directors.OrderBy(m => m.Name).Include(m => m.Movies).ToListAsync();

            if (directorDetails.Any())
                return directorDetails;

            else
                return null;

        }

    }
}

