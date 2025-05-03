using Microsoft.Extensions.Diagnostics.HealthChecks;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Services
{
    public interface IMovieCastRepository
    {


        Task<Cast?> GetCastDetailsByIdAsync(int id);

        Task<Cast?> UpdateCastDetailsByIdAsync(int id , CastUpdateDTO castUpdateDetails);

        void DeleteCastDetailsByIdAsync(int id , Cast castDetails);

        Task<bool> SaveChangesAsync();

    }
}
