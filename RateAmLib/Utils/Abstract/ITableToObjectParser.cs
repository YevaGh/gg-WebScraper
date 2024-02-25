namespace RateAmLib.Utils.Abtract
{
    public interface ITableToObjectParser
    {
        public Rate[] GetRates(Table table, bool latestTable = false);
    }
}
