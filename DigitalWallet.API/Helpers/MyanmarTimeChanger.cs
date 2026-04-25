namespace DigitalWallet.API.Helpers
{
    public class MyanmarTimeChanger
    {
        public static DateTime ConvertToMyanmarTime(DateTime utcDateTime)
        {
            DateTime myanmarTime = utcDateTime.AddHours(6).AddMinutes(30);
            return myanmarTime;
        }
    }
}
