using DigitalWallet.API.Common;
using DigitalWallet.API.Data;
using DigitalWallet.API.DTOs.Transactions;
using DigitalWallet.API.Enums;
using DigitalWallet.API.Helpers;
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
            if (request.Amount <= 0)
            {
                return Result<string>.Fail("Amount must be greater than zero");
            }

            var sender = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(x => x.UserId == senderUserId);
            if (sender == null)
            {
                return Result<string>.Fail("Sender not found");
            }

            var receiver = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(x => x.MobileNumber == request.MobileNumber);
            if (receiver == null)
            {
                return Result<string>.Fail("Receiver not found");
            }

            if (sender.UserId == receiver.UserId)
            {
                return Result<string>.Fail("Cannot transer to yourself");
            }

            if (sender.Status != Enums.UserStatus.Active || receiver.Status != Enums.UserStatus.Active)
            {
                return Result<string>.Fail("Sender or Receiver is not active");
            }

            if (sender.Wallet.Balance < request.Amount)
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

        public async Task<Result<PagedTransactionsResponse<GetTransactionsResponseDto>>> GetTransactionsAsync(int userId, GetTransactionsRequestDto request)
        {
            var user = await _context.Users
                        .Include(u => u.Wallet)
                        .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null)
            {
                return Result<PagedTransactionsResponse<GetTransactionsResponseDto>>.Fail("User not found");
            }
            if (user.Status != UserStatus.Active)
            {
                return Result<PagedTransactionsResponse<GetTransactionsResponseDto>>.Fail("User is not active");

            }
            var walletId = user.Wallet.WalletId;
            var query =  _context.Transactions
                        .Where(t => t.FromWalletId == walletId || t.ToWalletId == walletId);

            if(request.FromDate.HasValue)
            {
                var from = request.FromDate.Value.Date;
                query = query.Where(t => t.Timestamp >= from);
            }
            if(request.ToDate.HasValue)
            {
                var to = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(t => t.Timestamp <= to);
            }
            if(request.Type.HasValue)
            {
                query = query.Where(t => t.TransactionType == request.Type.Value);
            }

            var pageSize = Math.Clamp(request.PageSize, 1, 100);

            var totalCount = await query.CountAsync();

            var transactions = await query.OrderByDescending(t => t.Timestamp)
                                .Skip((request.PageNumber - 1) * pageSize)
                                .Take(request.PageSize)
                                .Select(t => new GetTransactionsResponseDto
                                {
                                    Amount = t.Amount,
                                    ToWalletId = t.ToWalletId,
                                    FromWalletId = (int)t.FromWalletId!,
                                    CreatedAt = t.Timestamp,
                                    Type = t.TransactionType.ToString()
                                }).ToListAsync();

            foreach (var tx in transactions)
            {
                tx.CreatedAt = MyanmarTimeChanger.ConvertToMyanmarTime(tx.CreatedAt);
            }
            var response = new PagedTransactionsResponse<GetTransactionsResponseDto>
            {
                Data = transactions,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result<PagedTransactionsResponse<GetTransactionsResponseDto>>.Success(response);
        }
    }
}
