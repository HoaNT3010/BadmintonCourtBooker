using Application.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Api controller for testing purposes.
    /// </summary>
    [Route("api/v1/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Throw an exception with custom status code, title and message response.
        /// </summary>
        /// <returns>Return a response with status code, title and message tailored to a HTTP status code (ex: NotFound HTTP status code).</returns>
        /// <exception cref="NotFoundException">Custom exception for HTTP status code NotFound (404).</exception>
        [HttpGet("error-handler")]
        public IActionResult TestErrorHandlerResponse()
        {
            throw new NotFoundException("Test NotFoundException response");
        }
    }
}
