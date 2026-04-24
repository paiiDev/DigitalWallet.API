using DigitalWallet.API.Common;

namespace DigitalWallet.API.Features.Wallets
{
    public interface IGetBalance
    {

        Task<Result<decimal>> GetBalanceAsync(int userId);
    }
}
