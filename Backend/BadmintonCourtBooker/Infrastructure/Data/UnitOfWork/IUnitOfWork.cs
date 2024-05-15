using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories
        public IUserRepository UserRepository { get; }
        #endregion

        #region Methods
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        #endregion
    }
}
