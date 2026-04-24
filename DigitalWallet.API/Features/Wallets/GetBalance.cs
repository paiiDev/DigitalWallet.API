using DigitalWallet.API.Common;
using DigitalWallet.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.API.Features.Wallets
{
    public class GetBalance : IGetBalance
    {
        private readonly AppDbContext _context;
        public GetBalance(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<decimal>> GetBalanceAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null)
            {
                return Result<decimal>.Fail("User not found.");
            }
            if (user.Status != Enums.UserStatus.Active)
            {
                return Result<decimal>.Fail("User is not active");
            }
            return Result<decimal>.Success(user.Wallet.Balance);
        }
    }
}
