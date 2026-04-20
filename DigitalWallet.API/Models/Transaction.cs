using DigitalWallet.API.Enums;

namespace DigitalWallet.API.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int? FromWalletId { get; set; }
        public int ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public TransactionType TransactionType { get; set; }
        public Wallet? FromWallet { get; set; }
        public Wallet ToWallet { get; set; } = null!;
    }
}
