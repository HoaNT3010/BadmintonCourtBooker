using Application.ErrorHandlers;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.ResponseDTOs.CourtStaff;
using Application.Services.ConcreteClasses;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/v1/staff")]
    [ApiController]
    public class CourtStaffController : ControllerBase
    {
        private readonly ICourtStaffService staffService;
        public CourtStaffController(ICourtStaffService courtStaffService)
        {
            this.staffService = courtStaffService;
        }
        /// <summary>
        /// View Court Status by Staff Reponse for this Court
        /// </summary>
        /// <param name="id">Staff ID.</param>
        /// <returns>Result is status of court.</returns>
        [HttpPost]
        [Route("{id:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtStaff)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtCreateResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<StatsCourtResponse>> ViewCourt([FromRoute] Guid id)
        {
            var result = await staffService.ViewStatsOfCourt(id);
            var slot = result.Item1; var book = result.Item2;
            return Ok(new { Slot = slot, Booking = book });
        }
        /// <summary>
        /// Checkin when customer u
        /// </summary>
        /// <param name="id">Staff ID.</param>
        /// <param name="bookingId">booking ID.</param>
        /// <returns>Result is status of court.</returns>
        [HttpPost]
        [Route("checkin/")]//{bookingid:guid
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtStaff)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtCreateResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<StatsCourtResponse>> CheckinCourt([FromRoute] Guid id, Guid bookingid)
        {
            var result = await staffService.CourtCheckin(id, bookingid);
            return Ok(result);
        }
    }
}
