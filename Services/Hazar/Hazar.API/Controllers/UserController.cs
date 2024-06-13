using Application.Contracts;
using Domain.Request.Users;
using Domain.Response.Users;
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


    }
}
