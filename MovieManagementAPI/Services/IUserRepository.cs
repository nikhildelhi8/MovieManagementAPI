using MovieManagementAPI.Entities;

namespace MovieManagementAPI.Services
{
    public interface IUserRepository
    {

        Task<User?> GetUserByUsernameAsync(string username);

        Task AddUserAsync(User user);
    }
}
