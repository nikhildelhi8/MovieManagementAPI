using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Entities;

namespace MovieManagementAPI.Services
{




    public class UserRepository : IUserRepository
    {
        private readonly MovieDbContext _context;

        public UserRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
