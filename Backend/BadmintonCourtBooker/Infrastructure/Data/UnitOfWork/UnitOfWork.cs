using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext context;
        private IDbContextTransaction? databaseTransaction = null;

        public IUserRepository UserRepository { get; } = null!;
        public ICourtRepository CourtRepository { get; } = null!;

        public IScheduleRepository ScheduleRepository { get; } = null!;

        public ISlotRepository SlotRepository { get; } = null!;

        public UnitOfWork(ApplicationDbContext context,
            IUserRepository userRepository,
            ICourtRepository courtRepository,
            IScheduleRepository scheduleRepository,
            ISlotRepository slotRepository)
        {
            this.context = context;
            UserRepository = userRepository;
            CourtRepository = courtRepository;
            ScheduleRepository = scheduleRepository;
            SlotRepository = slotRepository;
        }


        public async Task BeginTransactionAsync()
        {
            try
            {
                databaseTransaction = await context.Database.BeginTransactionAsync();
                Log.Information("Begin database transaction with ID: {transactionId} at {timeStamp}.", databaseTransaction.TransactionId, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error when trying to start database transaction at {timeStamp}. Detail: {exceptionDetail}", DateTime.UtcNow, ex.Message);
                throw new Exception("Unexpected error when trying to start database transaction.");
            }
        }

        public async Task CommitAsync()
        {
            if (databaseTransaction == null)
            {
                Log.Information("There are no initialized database transaction to commit");
                return;
            }
            try
            {
                Log.Information("Commit database transaction with ID: {transactionId} at {timeStamp}.", databaseTransaction.TransactionId, DateTime.UtcNow);
                await databaseTransaction!.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error when trying to commit database transaction with ID: {transactionId} at {timeStamp}. Detail: {exceptionDetail}", databaseTransaction.TransactionId, DateTime.UtcNow, ex.Message);
                throw new Exception("Unexpected error when trying to commit database transaction.");
            }
        }

        public async Task RollbackAsync()
        {
            if (databaseTransaction == null)
            {
                Log.Information("There are no initialized database transaction to rollback");
                return;
            }
            try
            {
                await databaseTransaction!.RollbackAsync();
                Log.Information("Rollback database transaction with ID: {transactionId} at {timeStamp}.", databaseTransaction.TransactionId, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error when trying to rollback database transaction with ID: {transactionId} at {timeStamp}. Detail: {exceptionDetail}", databaseTransaction.TransactionId, DateTime.UtcNow, ex.Message);
                throw new Exception("Unexpected error when trying to rollback database transaction.");
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
