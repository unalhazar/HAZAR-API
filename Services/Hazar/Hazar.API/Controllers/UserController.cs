using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.LogoutUser;
using Application.Features.Users.Commands.RefreshToken;
using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Requests;
using Application.Features.Users.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
        {
            var result = await mediator.Send(new LoginUserCommand(request));
            if (!result.Flag)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserRequest request)
        {
            var result = await mediator.Send(new RegisterUserCommand(request));
            if (!result.Flag)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> LogoutUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await mediator.Send(new LogoutUserCommand(token));
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await mediator.Send(new RefreshTokenCommand(request));
            if (!response.Success)
            {
                return Unauthorized(new { message = response.Message });
            }
            return Ok(new { token = response.JwtToken, refreshToken = response.RefreshToken });
        }
    }
}
