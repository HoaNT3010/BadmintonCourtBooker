using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<User?> Login(string email, string password);
    }
}
