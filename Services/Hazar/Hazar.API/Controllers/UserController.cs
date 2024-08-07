using Application.Abstraction;
using Domain.Request.Users;
using Domain.Response.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;

        public UserController(IUser user)
        {
            this.user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
        {
            var result = await user.LoginUserAsync(request);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserRequest request)
        {
            var result = await user.RegisterUserAsync(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<LogoutResponse>> LogoutUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var result = await user.LogoutUserAsync(token);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await user.RefreshTokenAsync(request);
            if (!response.Success)
            {
                return Unauthorized(new { message = response.Message });
            }
            return Ok(new { token = response.JwtToken, refreshToken = response.RefreshToken });
        }




    }
}
