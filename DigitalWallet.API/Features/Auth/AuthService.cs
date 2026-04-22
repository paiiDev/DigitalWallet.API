using DigitalWallet.API.Common;
using DigitalWallet.API.Data;
using DigitalWallet.API.DTOs.Auth;
using DigitalWallet.API.Enums;
using DigitalWallet.API.Helpers;
using DigitalWallet.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.API.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request)
        {
            var exist = await _context.Users.AnyAsync(x => x.MobileNumber == request.MobileNumber);
            if (exist)
            {
                return Result<RegisterResponseDto>.Fail("Mobile number already registered");
            }

            var user = new User
            {
                UserName = request.UserName,
                MobileNumber = request.MobileNumber,
                PinCodeHash = PasswordHasher.Hash(request.PinCode),
                Wallet = new Wallet
                {
                    Balance = 0
                }
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new RegisterResponseDto
            {
                UserId = user.UserId,
                MobileNumber = user.MobileNumber,
                UserName = user.UserName
            };
            return Result<RegisterResponseDto>.Success(response);
        }

        public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.MobileNumber == request.MobileNumber);

            if(user is null)
            {
                return Result<LoginResponseDto>.Fail("Invalid mobile number or pin code");
            }

            var isValidPin = PasswordHasher.Verify(request.PinCode, user.PinCodeHash);
            if (!isValidPin)
            {
                return Result<LoginResponseDto>.Fail("Invalid mobile number or pin code");
            }

            if(user.Status != UserStatus.Active)
            {
                return Result<LoginResponseDto>.Fail("User account is not active");
            }

            var response = new LoginResponseDto
            {
                UserId = user.UserId,
                MobileNumber = user.MobileNumber,
                UserName = user.UserName
            };

            return Result<LoginResponseDto>.Success(response);
        }
    }
}
