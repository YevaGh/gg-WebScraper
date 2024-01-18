using HtmlAgilityPack;

namespace RateAmLib
{
    public class DataParser
    {

        public static string[,] ScrapeRateAm()
        {
            string url = "https://rate.am/en/armenian-dram-exchange-rates/banks/non-cash";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //menak table i row nery
            List<HtmlNode> tableHtml = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='rb']")
            .Descendants("tr")
            .Skip(2)
            .Where(tr => tr.Elements("td").Count() > 1)
            .ToList();

            ;

            string[,] TableData = new string[18, 10];

            for (int i = 0; i < 18; i++)
            {
                var row = tableHtml[i].SelectNodes("th|td").ToList();
                var k = 0;

                for (int j = 0; j < 13; j++)
                {
                    if (j == 0 || j == 2 || j == 3)
                    {
                        continue;
                    }

                    TableData[i, k] = row[j].InnerText.Trim();
                    k++;

                }
            }

            return TableData;

        }



        public static List<Rate> DataToObject(string[,] data)
        {
            int i = 0;
            var rate_id = 0;
            List<Rate> rates = new List<Rate>();


            while (i < 18)
            {
                Console.WriteLine(data[i, 0]);
                var date = data[i, 1];
                Bank bank = Banks.BankSource.First(obj => obj.Name == data[i, 0]);
                var cur_id = 0;

                //4 currency from every row 
                for (int j = 2; j < 10; j += 2)
                {
                    cur_id++;
                    var buy_rate = data[i, j];
                    var sell_rate = data[i, j + 1];
                    var currency = Currencies.CurrenciesSource.First(obj => obj.Id == cur_id);

                    Rate rate = new Rate() { Id = rate_id++, Date = date, BuyRate = Convert.ToDecimal(buy_rate), SellRate = Convert.ToDecimal(sell_rate), Bank = bank, BankId = bank.Id, Currency = currency, CurrencyId = cur_id };

                    rates.Add(rate);
                    bank.Rates.Add(rate);
                    currency.Rates.Add(rate);

                }
                i++;


            }

            return rates;
        }

    }

}
