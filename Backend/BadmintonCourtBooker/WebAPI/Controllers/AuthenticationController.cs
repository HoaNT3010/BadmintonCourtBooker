﻿using Application.ErrorHandlers;
using Application.RequestDTOs.Auth;
using Application.ResponseDTOs;
using Application.ResponseDTOs.Auth;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthenticationController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Log user into the BadmintonCourtBooker system
        /// </summary>
        /// <param name="loginRequest">User's login credentials</param>
        /// <returns>Login result object contains JWT access token, user's role and status</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserLoginRequest loginRequest)
        {
            var result = await userService.UserLogin(loginRequest);
            return Ok(result);
        }

        /// <summary>
        /// Register new customer account for BadmintonCourtBooker system
        /// </summary>
        /// <param name="registerRequest">Customer's account information</param>
        /// <returns>Newly created customer's account data</returns>
        [HttpPost]
        [Route("register/customer")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CustomerRegisterResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetail))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetail))]
        public async Task<ActionResult<CustomerRegisterResponse>> RegisterCustomer([FromBody] CustomerRegisterRequest registerRequest)
        {
            var result = await userService.RegisterCustomer(registerRequest);
            return Ok(result);
        }
    }
}
