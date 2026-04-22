namespace DigitalWallet.API.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string MobileNumber { get; set; } = null!;
        public string PinCode { get; set; } = null!;
    }
}
