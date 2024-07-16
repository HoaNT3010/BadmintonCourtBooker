using Application.ErrorHandlers;
using Application.RequestDTOs.MoMo;
using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.MoMo;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
using Infrastructure.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Reflection;
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

        /// <summary>
        /// Retrieve a MoMo payment for the customer to process a booking transaction. Only Verified Customer can use this method.
        /// </summary>
        /// <param name="id">Id of booking transaction.</param>
        /// <returns>Information of MoMo payment,</returns>
        [HttpPost]
        [Route("process/{id:guid}/momo-payment")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MoMoCreatePaymentResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<MoMoCreatePaymentResponse>> CreateBookingTransactionMoMoPayment([FromRoute] Guid id)
        {
            var result = await transactionService.CreateMoMoPaymentForBookingTransaction(id);
            return Ok(result);
        }

        /// <summary>
        /// DO NOT USE THIS ENDPOINT! Only for MoMo to sent payment result.
        /// </summary>
        /// <param name="ipnRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("momo/ipn")]
        public async Task<ActionResult> MoMoResponseIpn([FromBody] MoMoIpnRequest ipnRequest)
        {
            await transactionService.ProcessMoMoPaymentResponse(ipnRequest);
            return NoContent();
        }

        /// <summary>
        /// DO NOT USE THIS ENDPOINT! Only for MoMo to sent payment result. 
        /// </summary>
        /// <param name="ipnRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("momo/return")]
        public async Task<ActionResult> MoMoResponseReturn([FromQuery] MoMoIpnRequest ipnRequest)
        {
            Log.Information("Received MoMo payment transaction response from HTTP GET return method!");
            return NoContent();
        }

        /// <summary>
        /// Request a recharge request for customer's booking time with MoMo payment method. Only Verified Customer can use this method.
        /// </summary>
        /// <param name="rechargeRequest">Data of recharge request.</param>
        /// <returns>MoMo payment method for recharge request.</returns>
        [HttpPost]
        [Route("create/time-recharge/momo")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MoMoCreatePaymentResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<MoMoCreatePaymentResponse>> CreateBookingTimeRechargeRequest([FromBody] BookingTimeRechargeRequest rechargeRequest)
        {
            var result = await transactionService.HandleBookingTimeRechargeRequest(rechargeRequest);
            return Ok(result);
        }

        /// <summary>
        ///  Cancel a pending transaction created by current user. Only Verified Customer can use this method.
        /// </summary>
        /// <param name="id">Id of transaction.</param>
        /// <returns>Transaction summary after cancelation.</returns>
        [HttpPut]
        [Route("{id:guid}/cancel")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionSummary))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<TransactionSummary>> CancelTransaction([FromRoute] Guid id)
        {
            var result = await transactionService.CancelTransaction(id);
            return Ok(result);
        }
    }
}
