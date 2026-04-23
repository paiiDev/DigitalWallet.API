namespace DigitalWallet.API.Features.Auth
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string mobileNumer);
    }
}
