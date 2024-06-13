using Application.ErrorHandlers;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.Services.Interfaces;
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
    }
}
