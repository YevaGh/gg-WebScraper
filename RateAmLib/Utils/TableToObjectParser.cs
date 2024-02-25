
using Microsoft.VisualBasic;
using RateAmLib.Utils.Abtract;
using System.Globalization;

namespace RateAmLib.Utils
{
    public class TableToObjectParser : ITableToObjectParser
    {
        public Rate[] GetRates(Table table,bool latestTable = false)
        {
            List<Rate> rates = new List<Rate>();

            var firstRow = table.Rows[0].Cells;
            var currencies = GetCurrenciesFromTable(firstRow);

            for (int i = 2; i < 20; i++)
            {
                var row = table.Rows[i];
                var bank = Banks.BankSource.First(obj => obj.Name == row.Cells[1].Value.Trim());
                var input = row.Cells[4].Value;
                var format = "dd MMM, HH:mm";
                DateTime date;
                DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                date = date.AddHours(+4).ToUniversalTime();
                var k = 0;
                var cellsLength = latestTable ? row.Cells.Length -2: row.Cells.Length;
                for (int j = 5; j < cellsLength; j += 2)
                {
                    decimal? buy_rate = null;
                    decimal? sell_rate = null;
                    var curr = currencies[k++];

                    buy_rate = TryParseDecimal(row.Cells[j].Value);
                    sell_rate = TryParseDecimal(row.Cells[j + 1].Value);


                    Rate rate = new Rate() { BuyRate = buy_rate,PublishDate = date, SellRate = sell_rate, BankId = bank.Id, CurrencyId = curr.Id };
                    rates.Add(rate);

                }

            }

            return rates.ToArray();

        }

        private static decimal TryParseDecimal(string input)
        {
            decimal val;
            if(decimal.TryParse(input, out val))
            {
                return val;
            }

            return 0;
        }

        private Currency[] GetCurrenciesFromTable(TableCell[] cells)
        {
            List<Currency> currencies = new List<Currency>();

            for (int i = 4; i < cells.Length; i++)
            {
                currencies.Add(Currencies.CurrenciesSource.First(curr => curr.Name == cells[i].Value));
            }

            return currencies.ToArray();
        }
    }
}
