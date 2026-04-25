using DigitalWallet.API.Enums;

namespace DigitalWallet.API.DTOs.Transactions
{
    public class GetTransactionsRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public DateTime? FromDate { get; set; } 
        public DateTime? ToDate { get; set; }
        public TransactionType? Type { get; set; }
    }
}
