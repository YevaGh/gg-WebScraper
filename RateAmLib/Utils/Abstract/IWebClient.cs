using RateAmLib.Enums;

namespace RateAmLib.Utils.Abtract
{
    public interface IWebClient
    {
        Task<string[]> GetPagesAsync();
        Task<string> GetPageAsync();
    }
}
