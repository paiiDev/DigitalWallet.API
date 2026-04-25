namespace DigitalWallet.API.DTOs.Transactions
{
    public class GetTransactionsRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
