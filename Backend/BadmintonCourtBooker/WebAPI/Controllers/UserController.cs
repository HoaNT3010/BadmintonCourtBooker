using Application.ErrorHandlers;
using Application.RequestDTOs.Auth;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs;
using Application.ResponseDTOs.Court;
using Application.Services.ConcreteClasses;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.OptionsSetup.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        /// <summary>
        /// Find current user profile. Only verified customer can use this feature.
        /// </summary>
        /// <returns>Result of get current user profile process.</returns>
        [HttpGet]
        [Route("profile")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedCustomer)]        
        public async Task<ActionResult<User>> GetCurrentUserProfileById()
        {
            var result = await userService.GetCurrentUserProfileById();
            return Ok(result);
        }

        /// <summary>
        /// Find exist user profile. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to get.</param>
        /// <returns>Result of get profile by id process.</returns>
        [HttpGet]
        [Route("detail/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> GetUserDetailById([FromRoute] Guid idRequest)
        {
            var result = await userService.GetUserDetailById(idRequest);
            return Ok(result);
        }

        /// <summary>
        /// Get list user. Only Administrator can use this feature.
        /// </summary>
        /// <returns>Result list of user.</returns>
        [HttpGet]
        [Route("get-all")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<ListCustomerResponse>> GetListUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {   
            var result = await userService.GetListUser(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Ban exist user. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to be banned.</param>
        /// <returns>Result of ban user by id process.</returns>
        [HttpPost]
        [Route("ban/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> BanUserById([FromRoute] Guid idRequest)
        {
            var result = await userService.BanUserById(idRequest);
            return Ok(result);
        }


        /// <summary>
        /// Search exist user. Only Administrator can use this feature.
        /// </summary>
        /// <returns>Result list of user.</returns>
        [HttpGet]
        [Route("search")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> SearchByNameByPhoneByEmail([FromQuery] SearchCustomerRequest searchCustomerRequest,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await userService.SearchByNameByEmailByPhone(searchCustomerRequest,pageNumber, pageSize);
            return Ok(result);
        }


        /// <summary>
        /// update exist user. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to be update.</param>
        /// <returns>Result of update user by id process.</returns>
        [HttpPost]
        [Route("update/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> UpdateUserById([FromRoute] Guid idRequest, [FromQuery] CustomerRegisterRequest customer)
        {
            var result = await userService.UpdateUserById(idRequest, customer);
            return Ok(result);
        }

        /// <summary>
        /// update current user. Only Administrator can use this feature.
        /// </summary>
        /// <returns>Result of update current user by id process.</returns>
        [HttpPost]
        [Route("update")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.CourtAdministrator)]
        public async Task<ActionResult<User>> UpdateCurrentUserById([FromQuery] CustomerRegisterRequest customer)
        {
            var result = await userService.UpdateCurrentUserById(customer);
            return Ok(result);
        }
    }
}
