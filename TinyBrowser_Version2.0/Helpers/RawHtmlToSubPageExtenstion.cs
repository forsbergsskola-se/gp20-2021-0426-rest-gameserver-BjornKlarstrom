using System.Linq;

namespace TinyBrowser.Helpers{
    public static class RawHtmlToSubPageExtenstion{
        public static Link ConvertToSubPage(this string subPage){
            var link = subPage.Replace("<b>", "").Replace("</b>", "");
            var nameAndUri = link.Split("\">");
            
            return new Link{
                Uri = nameAndUri[0],
                Info = nameAndUri[1]
            };
        }
        public static bool IsSubPage(this string subPage){
            var doesntStartWith = new[]{"//","http","mailto"};
            return !doesntStartWith.Any(subPage.StartsWith) && !subPage.Contains("src=\"");
        }
    }
}