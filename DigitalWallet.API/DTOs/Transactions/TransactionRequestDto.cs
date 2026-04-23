namespace DigitalWallet.API.DTOs.Transactions
{
    public class TransactionRequestDto
    {
        public string MobileNumber { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
