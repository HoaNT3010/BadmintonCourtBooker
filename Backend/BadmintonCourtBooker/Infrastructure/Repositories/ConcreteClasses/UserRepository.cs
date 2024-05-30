using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> Login(string email, string password)
        {
            User? user = await GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }
    }
}
