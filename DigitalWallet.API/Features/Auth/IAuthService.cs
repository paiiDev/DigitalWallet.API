using DigitalWallet.API.Common;
using DigitalWallet.API.DTOs.Auth;

namespace DigitalWallet.API.Features.Auth
{
    public interface IAuthService
    {
        Task<Result<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
