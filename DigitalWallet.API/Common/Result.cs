namespace DigitalWallet.API.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Value { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Value = value,
                Error = null
            };
        }
        public static Result<T> Fail(string error)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Value = default,
                Error = error
            };
        }
    }
}
