//using HtmlAgilityPack;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using HtmlAgilityPack;
using RateAmLib.Utils.Abtract;
using System.Xml;

namespace RateAmLib.Utils
{
    public class XmlDocumentParser : IXmlParser
    {
        public AngleSharp.Dom.IElement GetRatesTableNode(string xmlSource)
        {
            /*var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(xmlSource);
            return htmlDocument.DocumentNode.SelectSingleNode("//table[@class='rb']");*/
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xmlSource);

            //var xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(xmlSource);
            //string xpathExpression = "//table[@class='rb']";
            //return xmlDoc.DocumentElement.SelectSingleNode(xpathExpression);

            var parser = new HtmlParser();
            var document = parser.ParseDocument(xmlSource);
            return document.QuerySelector("table.rb");

        }
    }
}
