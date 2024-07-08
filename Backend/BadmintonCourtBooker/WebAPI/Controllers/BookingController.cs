using Application.ErrorHandlers;
using Application.RequestDTOs.Booking;
using Application.ResponseDTOs.Booking;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        /// <summary>
        /// Create a singular booking for a badminton court's slot within a specific day. Use for booking methods Day (Lịch từng ngày - thanh toán bằng tiền) and Flexible (Lịch linh hoạt - thanh toán bằng thời gian).
        /// Only verified customer can use this.
        /// </summary>
        /// <param name="courtId">Booking court's ID.</param>
        /// <param name="bookingRequest">New booking's information.</param>
        /// <returns>Result contains booking information, booking court and booking transaction.</returns>
        [HttpPost]
        [Route("create/singular/{courtId:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateBookingResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CreateBookingResponse>> CreateNewBooking([FromRoute] Guid courtId, [FromBody] CreateBookingRequest bookingRequest)
        {
            var result = await bookingService.CreateSingularBooking(courtId, bookingRequest);
            return Ok(result);
        }
    }
}
