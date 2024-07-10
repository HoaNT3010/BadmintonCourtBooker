using Application.ErrorHandlers;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.Data.UnitOfWork;

namespace Application.Services.ConcreteClasses
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IMapper mapper;

        public TransactionService(IJwtService jwtService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.jwtService = jwtService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region Query

        public async Task<TransactionSummary> GetPersonalFullTransaction(Guid transactionId)
        {
            var customerId = jwtService.GetCurrentUserId();
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionId);
            if (transaction == null)
            {
                throw new NotFoundException($"Cannot found information of transaction with ID '{transactionId.ToString()}'");
            }
            return mapper.Map<TransactionSummary>(transaction);
        }

        public async Task<TransactionSummary> GetPersonalFullTransactionByDetail(int transactionDetailId)
        {
            var customerId = jwtService.GetCurrentUserId();
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionDetailId);
            if (transaction == null)
            {
                throw new NotFoundException($"Cannot found information of transaction from transaction detail ID '{transactionDetailId.ToString()}'");
            }
            return mapper.Map<TransactionSummary>(transaction);
        }

        #endregion
    }
}
