using Application.ErrorHandlers;
using Application.RequestDTOs.Booking;
using Application.ResponseDTOs.Booking;
using Application.Services.Interfaces;
using Infrastructure.Utilities.Paging;
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

        /// <summary>
        /// Create multiple bookings for a same badminton court's slot but repeated for many weeks within a same day. Use for booking methods Fixed (Lịch cố định - thanh toán bằng tiền).
        /// Only verified customer can use this.
        /// </summary>
        /// <param name="courtId">Booking court's ID.</param>
        /// <param name="bookingRequest">New booking's information contains multiple rent dates.</param>
        /// <returns>Result contains bookings information, booking court and booking transaction.</returns>
        [HttpPost]
        [Route("create/multiple/{courtId:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateMultipleBookingResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CreateMultipleBookingResponse>> CreateNewMultipleBooking([FromRoute] Guid courtId, [FromBody] CreateMultipleBookingRequest bookingRequest)
        {
            var result = await bookingService.CreateMultipleBooking(courtId, bookingRequest);
            return Ok(result);
        }

        /// <summary>
        /// Get paginated list of created bookings of current customer. Only Verified Customer can use this feature.
        /// </summary>
        /// <param name="queryRequest">Contains query and sorting options.</param>
        /// <returns>List of customer's bookings.</returns>
        [HttpGet]
        [Route("personal")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<BookingShortDetail>))]
        public async Task<ActionResult<PagedList<BookingShortDetail>>> GetCurrentCustomerBookings([FromQuery] BookingQueryRequest queryRequest)
        {
            var result = await bookingService.GetCurrentCustomerBookings(queryRequest);
            return Ok(result);
        }

        /// <summary>
        /// Get a booking with full information of current customer. Only Verified Customer can use this feature.
        /// </summary>
        /// <param name="id">ID of customer's booking.</param>
        /// <returns>Full information of the booking.</returns>
        [HttpGet]
        [Route("personal/{id:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BookingDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<BookingDetail>> GetCurrentCustomerBookingDetail([FromRoute] Guid id)
        {
            var result = await bookingService.GetCurrentCustomerBookingsDetail(id);
            return Ok(result);
        }
    }
}
