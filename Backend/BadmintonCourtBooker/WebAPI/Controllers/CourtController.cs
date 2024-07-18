using Application.ErrorHandlers;
using Application.RequestDTOs.Auth;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.Services.ConcreteClasses;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/v1/courts")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService courtService;

        public CourtController(ICourtService courtService)
        {
            this.courtService = courtService;
        }

        /// <summary>
        /// Create new badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="createRequest">New court data to be added.</param>
        /// <returns>Result of the create new court process.</returns>
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtCreateResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtCreateResponse>> CreateCourt([FromBody] CourtCreateRequest createRequest)
        {
            var result = await courtService.CreateNewCourt(createRequest);
            return Ok(result);
        }

        /// <summary>
        /// Add schedules to an already existing badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <param name="createRequest">New schedules to be added to the court.</param>
        /// <returns>Court detail information with newly added schedules.</returns>
        [HttpPost]
        [Route("{id:guid}/add-schedules")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtDetail>> AddCourtSchedules([FromRoute] Guid id, [FromBody] CourtScheduleCreateRequest createRequest)
        {
            var result = await courtService.AddCourtSchedule(id, createRequest);
            return Ok(result);
        }

        /// <summary>
        /// Get badminton court detail information based on court's ID.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <returns>Court detail information</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtDetail>> GetCourtDetail(Guid id)
        {
            return Ok(await courtService.GetCourtDetail(id));
        }

        /// <summary>
        /// Add new employees to an already existing badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <param name="request">New employees to be added as court employees.</param>
        /// <returns>Court detail information with newly added employees.</returns>
        [HttpPost]
        [Route("{id:guid}/add-employees")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtDetail>> AddCourtEmployees([FromRoute] Guid id, [FromBody] AddCourtEmployeeRequest request)
        {
            return Ok(await courtService.AddCourtEmployees(id, request));
        }

        /// <summary>
        /// Add new payment methods to an already existing badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <param name="request">New payment methods to be added as court payment methods.</param>
        /// <returns>Court detail information with newly added payment methods.</returns>
        [HttpPost]
        [Route("{id:guid}/add-payment-methods")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtDetail>> AddCourtPaymentMethods([FromRoute] Guid id, [FromBody] PaymentMethodCreateRequest request)
        {
            return Ok(await courtService.AddCourtPaymentMethods(id, request));
        }

        /// <summary>
        /// Add new booking methods to an already existing badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <param name="request">New booking methods to be added as court booking methods.</param>
        /// <returns>Court detail information with newly added booking methods.</returns>
        [HttpPost]
        [Route("{id:guid}/add-booking-methods")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CourtDetail>> AddCourtBookingMethods([FromRoute] Guid id, [FromBody] BookingMethodCreateRequest request)
        {
            return Ok(await courtService.AddCourtBookingMethods(id, request));
        }

        /// <summary>
        /// Activate a badminton court (Change status to Active), the court must satisfied conditions to be activated. Only court manager and system admin can use this feature. 
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <returns>Message contains the result</returns>
        /// <exception cref="Exception"></exception>
        [HttpPatch]
        [Route("{id:guid}/activate")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MessageDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<MessageDetail>> ActivateBadmintonCourt([FromRoute] Guid id)
        {
            var result = await courtService.ActivateCourt(id);
            if (!result)
            {
                throw new Exception("Failed to activate court. Please try again!");
            }
            return Ok(new MessageDetail()
            {
                StatusCode = 200,
                Title = "Success",
                Message = "Successfully activate court",
            });
        }

        /// <summary>
        /// Deactivate a badminton court (Change status to Inactive). Only court manager and system admin can use this feature. 
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <returns>Message contains the result</returns>
        /// <exception cref="Exception"></exception>
        [HttpPatch]
        [Route("{id:guid}/deactivate")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MessageDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<MessageDetail>> DeactivateBadmintonCourt([FromRoute] Guid id)
        {
            var result = await courtService.DeactivateCourt(id);
            if (!result)
            {
                throw new Exception("Failed to deactivate court. Please try again!");
            }
            return Ok(new MessageDetail()
            {
                StatusCode = 200,
                Title = "Success",
                Message = "Successfully deactivate court",
            });
        }

        /// <summary>
        /// Search courts with various filters, paging option and sorting order.
        /// </summary>
        /// <param name="searchRequest">Search request contains filters, sorting order and paging option.</param>
        /// <returns>Paged list of badminton courts.</returns>
        [HttpGet]
        [Route("search")]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<CourtShortDetail>))]
        public async Task<ActionResult<PagedList<CourtShortDetail>>> SearchCourts([FromQuery] CourtSearchRequest searchRequest)
        {
            var result = await courtService.SearchCourt(searchRequest);
            return Ok(result);
        }

        /// <summary>
        /// Delete a badminton court by ID. The deleted court will have status changed to Removed and no longer be interacted with. Only court manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">ID of badminton court.</param>
        /// <returns>Message contains the result</returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete]
        [Route("{id:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MessageDetail))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<MessageDetail>> SoftDeleteCourt([FromRoute] Guid id)
        {
            var result = await courtService.SoftDeleteCourt(id);
            if (!result)
            {
                throw new Exception("Failed to delete court. Please try again!");
            }
            return Ok(new MessageDetail()
            {
                StatusCode = 200,
                Title = "Success",
                Message = "Successfully delete court",
            });
        }

        /// <summary>
        /// update exist court. Only Court Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of court need to be update.</param>
        /// <returns>Result of update court by id process.</returns>
        [HttpPut]
        [Route("update/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> UpdateUserById([FromRoute] Guid idRequest, [FromBody] CourtUpdateRequest courtUpdateRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await courtService.UpdateCourtById(idRequest, courtUpdateRequest);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// Update payment methods to an already existing badminton court. Only manager and system admin can use this feature.
        /// </summary>
        /// <param name="id">Badminton court's ID.</param>
        /// <param name="requestPaymentMethodId">Id of payment methods to be added as court booking methods.</param>
        /// <returns>Court detail information with newly added booking methods.</returns>
        [HttpPut("{id:Guid}&{requestPaymentMethodId:int}/update-payment-methods")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourtDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetail))]
        [Authorize(Policy = AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<CourtDetail>> UpdateCourtBookingMethods([FromRoute] Guid id, int requestPaymentMethodId)
        {
            try
            {
                var courtDetail = await courtService.UpdateCourtPaymentMethods(id, requestPaymentMethodId);
                return Ok(courtDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDetail { Message = ex.Message });
            }
        }
    }
}
