using DigitalWallet.API.Enums;

namespace DigitalWallet.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PinCodeHash { get; set; } = null!;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Wallet Wallet { get; set; } = null!;
    }
}
