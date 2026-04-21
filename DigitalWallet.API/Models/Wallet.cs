
namespace DigitalWallet.API.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
    }
}
