namespace DigitalWallet.API.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string MobileNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PinCode { get; set; } = null!;
    }
}
