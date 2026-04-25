namespace DigitalWallet.API.DTOs.Transactions
{
    public class PagedTransactionsResponse<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } 
    }
}
