using Application.Mapper.Dtos;
using Application.User.Authentication;
using Application.User.GetUser;
using Application.User.GetUser.GetUserByEmail;
using Application.User.GetUserById.GetUser;
using Application.Users.RegisterNewUser;
using Domain.ValueObjects;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser([FromBody] AuthenticationCommand authenticationUser)
        {
            var response = await _mediator.Send(authenticationUser);
            return Ok(response);
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegisterNewUserCommandResponse>> RegisterNewUser([FromBody] RegisterNewUserCommand registerNewUserCommand)
        {
            var response = await _mediator.Send(registerNewUserCommand);
            return Ok(response);
        }

        [Authorize(Roles = "Coordinator")]
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var userDto = await _mediator.Send(new GetUserByIdQuery() {Id=id});
            return Ok(userDto);
        }
        [Authorize(Roles = "Coordinator")]
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var userDto = await _mediator.Send(new GetUserByEmailQuery() { Email = new Email(email) });
            return Ok(userDto);
        }
    }
}
