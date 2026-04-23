using DigitalWallet.API.Common;
using DigitalWallet.API.Data;
using DigitalWallet.API.DTOs.Transactions;
using DigitalWallet.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.API.Features.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> TransferAsync(int senderUserId, TransactionRequestDto request)
        {
            if(request.Amount <= 0)
            {
                return Result<string>.Fail("Amount must be greater than zero");
            }

            var sender = await _context.Users.Include( u => u.Wallet).FirstOrDefaultAsync(x => x.UserId == senderUserId);
            if (sender == null)
            {
                return Result<string>.Fail("Sender not found");
            }

            var receiver = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(x => x.MobileNumber == request.MobileNumber);
            if (receiver == null)
            {
                return Result<string>.Fail("Receiver not found");
            }

            if(sender.UserId == receiver.UserId)
            {
                return Result<string>.Fail("Cannot transer to yourself");
            }

            if( sender.Status != Enums.UserStatus.Active || receiver.Status != Enums.UserStatus.Active)
            {
                return Result<string>.Fail("Sender or Receiver is not active");
            }

            if(sender.Wallet.Balance < request.Amount)
            {
                return Result<string>.Fail("Insufficient balance");
            }

            using var dbTx = await _context.Database.BeginTransactionAsync();

            try
            {
                sender.Wallet.Balance -= request.Amount;
                receiver.Wallet.Balance += request.Amount;

                var tx = new Transaction
                {
                    FromWalletId = sender.Wallet.WalletId,
                    ToWalletId = receiver.Wallet.WalletId,
                    Amount = request.Amount,
                    TransactionType = Enums.TransactionType.Transfer
                };

                _context.Transactions.Add(tx);

                await _context.SaveChangesAsync();
                await dbTx.CommitAsync();

                return Result<string>.Success("Transfer successful.");
            }
            catch (Exception)
            {
                await dbTx.RollbackAsync();
                return Result<string>.Fail("Transaction failed. Please try again...");
            }
        }
    }
}
