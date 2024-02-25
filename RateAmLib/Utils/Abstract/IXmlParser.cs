//using HtmlAgilityPack;\
using System.Xml;

namespace RateAmLib.Utils.Abtract
{
    public interface IXmlParser
    {
        public AngleSharp.Dom.IElement GetRatesTableNode(string xmlSource);
    }
}
