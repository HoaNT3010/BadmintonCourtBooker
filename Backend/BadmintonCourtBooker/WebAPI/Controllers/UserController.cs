using Application.RequestDTOs.Auth;
using Application.ResponseDTOs;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedUser)]
        public async Task<ActionResult<ProfileResponse>> GetCurrentUserProfileById()
        {
            var result = await userService.GetCurrentUserProfileById();
            return Ok(result);
        }

        /// <summary>
        /// Find exist user profile. Only authorize user can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to get.</param>
        /// <returns>Result of get profile by id process.</returns>
        [HttpGet]
        [Route("detail/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedUser)]
        public async Task<ActionResult<ProfileResponse>> GetUserDetailById([FromRoute] Guid idRequest)
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
        [Authorize(policy: AuthorizationOptionsSetup.SystemAdministrator)]
        public async Task<ActionResult<ListCustomerResponse>> GetListUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await userService.GetListUser(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Ban or unban exist user. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to be banned.</param>
        /// <returns>Result of ban user by id process.</returns>
        [HttpPost]
        [Route("ban/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.SystemAdministrator)]
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
        [Authorize(policy: AuthorizationOptionsSetup.SystemAdministrator)]
        public async Task<ActionResult<User>> SearchByNameByPhoneByEmail([FromQuery] SearchCustomerRequest searchCustomerRequest, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await userService.SearchByNameByEmailByPhone(searchCustomerRequest, pageNumber, pageSize);
            return Ok(result);
        }


        /// <summary>
        /// update exist user. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to be update.</param>
        /// <returns>Result of update user by id process.</returns>
        [HttpPut]
        [Route("update/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.SystemAdministrator)]
        public async Task<ActionResult<User>> UpdateUserById([FromRoute] Guid idRequest, [FromBody] CustomerUpdateRequest customer)
        {
            var result = await userService.UpdateUserById(idRequest, customer);
            return Ok(result);
        }

        /// <summary>
        /// update current user. Only Administrator can use this feature.
        /// </summary>
        /// <returns>Result of update current user by id process.</returns>
        [HttpPut]
        [Route("update")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.VerifiedUser)]
        public async Task<ActionResult<User>> UpdateCurrentUserById([FromBody] CustomerUpdateRequest customer)
        {
            var result = await userService.UpdateCurrentUserById(customer);
            return Ok(result);
        }


        /// <summary>
        /// update role exist user. Only Administrator can use this feature.
        /// </summary>
        /// <param id="idRequest">Id of profile need to be update role.</param>
        /// <returns>Result of update role user by id process.</returns>
        [HttpPut]
        [Route("update-role/{idRequest:guid}")]
        [Produces("application/json")]
        [Authorize(policy: AuthorizationOptionsSetup.SystemAdministrator)]
        public async Task<ActionResult<User>> UpdateRoleUserById([FromRoute] Guid idRequest, UserRole role)
        {
            var result = await userService.UpdateRoleUserById(idRequest, role);
            return Ok(result);
        }
    }
}
