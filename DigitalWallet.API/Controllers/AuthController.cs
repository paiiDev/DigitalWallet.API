using DigitalWallet.API.Common;
using DigitalWallet.API.DTOs.Auth;
using DigitalWallet.API.Features.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result.IsSuccess)
            {
                return Ok(BaseResponse<RegisterResponseDto>.Ok(result.Value));
            }
            return BadRequest(BaseResponse<RegisterResponseDto>.Fail(result.Error));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (result.IsSuccess)
            {
                return Ok(BaseResponse<LoginResponseDto>.Ok(result.Value));
            }
            return BadRequest(BaseResponse<LoginResponseDto>.Fail(result.Error));
        }
    }
}
