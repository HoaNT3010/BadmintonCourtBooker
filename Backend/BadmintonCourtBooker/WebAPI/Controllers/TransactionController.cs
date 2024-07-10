using Application.ErrorHandlers;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
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
    }
}
