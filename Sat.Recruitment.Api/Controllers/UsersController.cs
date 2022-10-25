using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.UseCases.Interfaces;
using Sat.Recruitment.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// User related methods
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("2.0")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUserBusinessLogic _userBussinessLogic;
        /// <summary>
        /// UserController constructor
        /// </summary>
        public UsersController(IUserBusinessLogic userBussinessLogic)
        {
            _userBussinessLogic = userBussinessLogic;
        }

        // Post: v2.0/create-user
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <remarks>
        /// Creates new user validating duplicity and calculating total money
        /// </remarks>
        /// <param name="user">userDTO</param>
        /// <response code="200">User Created</response>        
        /// <response code="400">Validation error</response>
        [HttpPost]
        [MapToApiVersion("2.0")]
        [ApiExplorerSettings(GroupName = "v2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("v{version:apiVersion}/create-user")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            var response = await _userBussinessLogic.CreateUser(user);
            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }


    }
}
