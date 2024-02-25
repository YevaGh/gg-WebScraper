using RateAmLib.Utils.Abtract;

namespace RateAmLib.Utils
{
    public class HtmlTableParser : IHtmlTableParser
    {
        public Table ParseHtmlTable(AngleSharp.Dom.IElement htmlNode)
        {
            List<TableRow> rows = new List<TableRow>();            
            var htmlRows = htmlNode.QuerySelectorAll("tr").ToArray();
            for (var i = 0; i < htmlRows.Length; i++)
            {
                var htmlCells = htmlRows[i].QuerySelectorAll("td").ToArray();

                TableRow row = new TableRow();
                List<TableCell> cells = new List<TableCell>();

                for (var j = 0; j < htmlCells.Length; j++)
                {
                    var htmlCell = htmlCells[j];
                    cells.Add(new TableCell() { Value = GetCellValue(i, j, htmlCell) });
                }

                row.Cells = cells.ToArray();
                rows.Add(row);
            }

            return new Table() { Rows = rows.ToArray() };
        }

        private static string GetCellValue(int i, int j, AngleSharp.Dom.IElement node)
        {
            if(i == 0 && j > 3)
            {
                //vercnuma currency nery
                //return node.SelectSingleNode(".//option[@selected]").InnerHtml.Split(" ")[1];
                return node.QuerySelector("option[selected]").InnerHtml.Split(" ")[1];
            }
            if (i >= 2 && i <= 19 && j == 1)
            {
                return node.QuerySelector("a").InnerHtml;
            }

            return node.TextContent;
        }
    }


}
