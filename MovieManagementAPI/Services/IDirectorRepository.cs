using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Services
{
    public interface IDirectorRepository
    {


        Task<IEnumerable<Director>> GetAllDirectorDetailsAsync();


    }
}
