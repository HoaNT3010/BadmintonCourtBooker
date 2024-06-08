using Application.ErrorHandlers;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs;
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

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtCreator)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CourtCreateResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<IActionResult> CreateCourt([FromBody] CourtCreateRequest createRequest)
        {
            var result = await courtService.CreateNewCourt(createRequest);
            return Ok(result);
        }
    }
}
