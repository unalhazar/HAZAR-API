using Application.Contracts;
using Application.Contracts.Persistence;
using Domain.Request.Users;
using Domain.Response.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public UserController(IUser user, ITokenBlacklistService tokenBlacklistService)
        {
            this.user = user;
            _tokenBlacklistService = tokenBlacklistService;
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

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new LogoutResponse(false, "Token bulunamadı."));
            }

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo;

            await _tokenBlacklistService.AddTokenToBlacklist(token, expirationDate);

            return Ok(new LogoutResponse(true, "Çıkış başarılı."));
        }


    }
}
