﻿using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<User?> Login(string email, string password);
        Task<User?> GetByIdAsync(Guid id);
        IQueryable<User> GetListUser();
        IQueryable<User> SearchByNameByEmailByPhoneByStatus(string name, string email, string phone, UserStatus status);
    }
}
