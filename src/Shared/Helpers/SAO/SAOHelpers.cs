using HtmlAgilityPack;
using Shared.Interfaces.SAO;

namespace Shared.Helpers.SAO
{
    public class SAOHelpers : ISAOHelpers
    {
        public HtmlNode getNodeByXPath(string html, string xpath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.DocumentNode.SelectSingleNode(xpath);
        }

        public HtmlNodeCollection getNodeCollectionByXPath(string html, string xpath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.DocumentNode.SelectNodes(xpath);
        }
    }
}
