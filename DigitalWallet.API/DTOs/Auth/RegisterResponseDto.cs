namespace DigitalWallet.API.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
