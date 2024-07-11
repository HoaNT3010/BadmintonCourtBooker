using Application.ErrorHandlers;
using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Utilities.Paging;
using System.Linq.Expressions;

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

        public async Task<PagedList<TransactionShortSummary>?> GetPersonalTransactions(TransactionQueryRequest queryRequest)
        {
            var creatorId = jwtService.GetCurrentUserId();

            Expression<Func<Transaction, bool>> filterExpression = GetTransactionFilterExpression(creatorId, queryRequest);
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> sortingExpression = GetSortingExpression(queryRequest.OrderBy, queryRequest.SortingOrder);

            var queryResult = await unitOfWork.TransactionRepository.GetPaginatedAsync(queryRequest.PageNumber,
                queryRequest.PageSize,
                filterExpression,
                sortingExpression);

            return mapper.Map<PagedList<TransactionShortSummary>>(queryResult);
        }

        #endregion

        #region Utilities

        private Expression<Func<Transaction, bool>> GetTransactionFilterExpression(Guid? customerId, TransactionQueryRequest queryRequest)
        {
            Expression<Func<Transaction, bool>> baseExp = (t => t.CreatorId == customerId);
            Expression<Func<Transaction, bool>>? statusExp = queryRequest.Status == TransactionStatus.None ? null : (t => t.Status == queryRequest.Status);
            Expression<Func<Transaction, bool>>? methodTypeExp = queryRequest.MethodType == PaymentMethodType.None ? null : (t => t.PaymentMethod == queryRequest.MethodType);

            ParameterExpression parameter = Expression.Parameter(typeof(Transaction));
            Expression combinedBody = Expression.Invoke(baseExp, parameter);

            if (statusExp != null)
            {
                combinedBody = Expression.AndAlso(combinedBody, Expression.Invoke(statusExp, parameter));
            }

            if (methodTypeExp != null)
            {
                combinedBody = Expression.AndAlso(combinedBody, Expression.Invoke(methodTypeExp, parameter));
            }

            return Expression.Lambda<Func<Transaction, bool>>(combinedBody, parameter);
        }

        private Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> GetSortingExpression(TransactionOrderBy orderBy, SortingOrder sortingOrder)
        {
            //var orderings = new Dictionary<(TransactionOrderBy, SortingOrder), Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>>
            //{
            //    {(TransactionOrderBy.CreateDate, SortingOrder.Ascending), q => q.OrderBy(t => t.CreatedDate)},
            //    {(TransactionOrderBy.TotalAmount, SortingOrder.Ascending), q => q.OrderBy(t => t.TotalAmount)},
            //    {(TransactionOrderBy.TotalBookingTime, SortingOrder.Ascending), q => q.OrderBy(t => t.TotalBookingTime)},
            //    {(TransactionOrderBy.CreateDate, SortingOrder.Descending), q => q.OrderByDescending(t => t.CreatedDate)},
            //    {(TransactionOrderBy.TotalAmount, SortingOrder.Descending), q => q.OrderByDescending(t => t.TotalAmount)},
            //    {(TransactionOrderBy.TotalBookingTime, SortingOrder.Descending), q => q.OrderByDescending(t => t.TotalBookingTime)},
            //};

            //if (orderings.TryGetValue((orderBy, sortingOrder), out var sortingExpression))
            //{
            //    return sortingExpression;
            //}

            //return (q => q.OrderBy(t => t.CreatedDate));

            switch (orderBy)
            {
                case TransactionOrderBy.CreateDate:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.CreatedDate)) : (q => q.OrderByDescending(t => t.CreatedDate));
                case TransactionOrderBy.TotalAmount:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.TotalAmount)) : (q => q.OrderByDescending(t => t.TotalAmount));
                case TransactionOrderBy.TotalBookingTime:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.TotalBookingTime)) : (q => q.OrderByDescending(t => t.TotalBookingTime));
            }

            return (q => q.OrderByDescending(t => t.CreatedDate));
        }

        #endregion
    }
}
