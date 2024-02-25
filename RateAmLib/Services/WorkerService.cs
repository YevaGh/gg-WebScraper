using RateAmData.Repositories;
using RateAmData;
using RateAmLib.Utils;
using Microsoft.Extensions.Configuration;
using RateAmLib.Utils.Abtract;

namespace RateAmLib.Services
{
    public class WorkerService
    {
        private readonly IRateService _rateService;
        private readonly IWebClient _webClient;
        private readonly IXmlParser _xmlParser;
        private readonly IHtmlTableParser _tableParser;
        private readonly ITableToObjectParser _objectParser;

        public WorkerService(IRateService rateService,IWebClient webClient,IXmlParser xmlParser,IHtmlTableParser tableParser,ITableToObjectParser objectParser)
        {
            _rateService = rateService;
            _webClient = webClient;
            _xmlParser = xmlParser;
            _tableParser = tableParser;
            _objectParser = objectParser;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            
            //RateAmDataContext rateAmData = new RateAmDataContext(builderConfig);
            //IRatesRepository rateRepo = new RatesRepository(rateAmData);
            //IRateService rateService = new RateService(rateRepo);
            //IWebClient webClient = new SeleniumWebClient();
            //IXmlParser xmlParser = new XmlDocumentParser();
            //IHtmlTableParser tableParser = new HtmlTableParser();
            //ITableToObjectParser objectParser = new TableToObjectParser();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine($"Worker doing some work at {DateTime.Now}");

                    var pages = await _webClient.GetPagesAsync();
                    var rates = new List<Rate>();

                    for (int i = 0; i < pages.Length; i++)
                    {
                        var tableNode = _xmlParser.GetRatesTableNode(pages[i]);
                        var table = _tableParser.ParseHtmlTable(tableNode);
                        var ratesOfPage = _objectParser.GetRates(table, i == pages.Length - 1);
                        rates.AddRange(ratesOfPage);
                    }
                    await _rateService.SaveAll([.. rates]);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e}");
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                }
            }
        }
    }
}
