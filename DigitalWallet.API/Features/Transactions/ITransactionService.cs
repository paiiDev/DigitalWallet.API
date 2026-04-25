using DigitalWallet.API.Common;
using DigitalWallet.API.DTOs.Transactions;

namespace DigitalWallet.API.Features.Transactions
{
    public interface ITransactionService
    {
        Task<Result<string>> TransferAsync(int senderUserId, TransactionRequestDto request);
        Task<Result<PagedTransactionsResponse<GetTransactionsResponseDto>>> GetTransactionsAsync(int userId, GetTransactionsRequestDto request);
    }
}
