using HtmlAgilityPack;

namespace RateAmLib.Utils.Abtract
{
    public class Table
    {
        public TableRow[] Rows { get; set; }
    }
    public class TableCell
    {
        public string Value { get; set; }
    }

    public class TableRow
    {
        public TableCell[] Cells { get; set; }
    }
    public interface IHtmlTableParser
    {
        public Table ParseHtmlTable(AngleSharp.Dom.IElement htmlNode);
    }
}
