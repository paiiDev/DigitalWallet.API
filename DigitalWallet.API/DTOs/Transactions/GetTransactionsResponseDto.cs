using DigitalWallet.API.Enums;

namespace DigitalWallet.API.DTOs.Transactions
{
    public class GetTransactionsResponseDto
    {
        public int FromWalletId { get; set; }
        public int ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
    }
}
