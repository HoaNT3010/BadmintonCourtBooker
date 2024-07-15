using Application.ErrorHandlers;
using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
using Infrastructure.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        /// <summary>
        /// Retrieve a transaction with full information of current customer. Only Verified Customer can use this feature.
        /// </summary>
        /// <param name="id">ID of customer's transaction.</param>
        /// <returns>Gull information of the transaction.</returns>
        [HttpGet]
        [Route("personal/{id:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionSummary))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<TransactionSummary>> GetPersonalFullTransaction([FromRoute] Guid id)
        {
            var result = await transactionService.GetPersonalFullTransaction(id);
            return Ok(result);
        }

        /// <summary>
        /// Retrieve a transaction with full information of current customer using the transaction's transaction detail ID. Only Verified Customer can use this feature.
        /// </summary>
        /// <param name="id">ID of customer's transaction.</param>
        /// <returns>Gull information of the transaction.</returns>
        [HttpGet]
        [Route("personal/by-detail/{id:int}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionSummary))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<TransactionSummary>> GetPersonalFullTransactionByDetailId([FromRoute] int id)
        {
            var result = await transactionService.GetPersonalFullTransactionByDetail(id);
            return Ok(result);
        }

        /// <summary>
        /// Get paginated list of created transactions of current customer. Only Verified Customer can use this feature.
        /// </summary>
        /// <param name="queryRequest">Contains query and sorting options.</param>
        /// <returns>List of customer's transactions.</returns>
        [HttpGet]
        [Route("personal")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<TransactionShortSummary>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<PagedList<TransactionShortSummary>>> GetPersonalTransactions([FromQuery] TransactionQueryRequest queryRequest)
        {
            var result = await transactionService.GetPersonalTransactions(queryRequest);
            return Ok(result);
        }

        /// <summary>
        /// Process customer payment of a booking time transaction. Only Verified Customer can use this method.
        /// If there are any invalid booking in the transaction, the transaction and it's booking will be cancel.
        /// </summary>
        /// <param name="id">Id of transaction need to be process.</param>
        /// <returns>Transaction summary after being processed.</returns>
        [HttpPost]
        [Route("process/{id:guid}/booking-time")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionSummary))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<TransactionSummary>> ProcessBookingTimeTransaction([FromRoute] Guid id)
        {
            var result = await transactionService.ProcessBookingTimeTransaction(id);
            return Ok(result);
        }
    }
}
