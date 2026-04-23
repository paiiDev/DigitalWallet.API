namespace DigitalWallet.API.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; } 
        public string MobileNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
