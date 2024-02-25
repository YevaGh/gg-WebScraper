using RateAmLib.Utils.Abtract;

namespace RateAmLib.Utils
{
    public class HttpWebClient : IWebClient
    {
        public async Task<string> GetPageAsync()
        {
            string url = "https://rate.am/en/armenian-dram-exchange-rates/banks/non-cash";
            var httpClient = new HttpClient();
            return await (httpClient.GetStringAsync(url));
        }

        public Task<string[]> GetPagesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
