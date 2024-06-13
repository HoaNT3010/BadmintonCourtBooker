using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories
        public IUserRepository UserRepository { get; }
        public ICourtRepository CourtRepository { get; }
        public IScheduleRepository ScheduleRepository { get; }
        public ISlotRepository SlotRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IBookingMethodRepository BookingMethodRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public IBookingRepository BookingRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public ITransactionDetailRepository TransactionDetailRepository { get; }
        #endregion

        #region Methods
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        void Dispose();
        #endregion
    }
}
