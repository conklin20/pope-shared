using HtmlAgilityPack;

namespace Shared.Interfaces.SAO
{
    public interface ISAOHelpers
    {
        HtmlNode getNodeByXPath(string html, string xpath);
        HtmlNodeCollection getNodeCollectionByXPath(string html, string xpath);
    }
}
