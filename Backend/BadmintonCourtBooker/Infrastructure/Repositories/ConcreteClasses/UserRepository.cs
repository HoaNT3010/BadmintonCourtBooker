using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

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

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<User?> Login(string email, string password)
        {
            User? user = await GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            if (!BC.EnhancedVerify(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Id == id);
        }

        public IQueryable<User> GetListUser()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<User> SearchByNameByEmailByPhone(string name, string email, string phone)
        {
            var query = dbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(c => c.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FirstName.Contains(name));
            }

            return query;
        }

    }
}
