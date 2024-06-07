using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
