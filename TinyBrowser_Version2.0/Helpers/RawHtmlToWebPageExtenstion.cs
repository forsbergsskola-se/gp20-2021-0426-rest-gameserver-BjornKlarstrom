using System;
using System.Collections.Generic;
using TinyBrowser.Interfaces;

namespace TinyBrowser.Helpers{
    public static class RawHtmlToWebPageExtenstion{
        static string rawHtml;

        public static IPage ConvertHtmlToWebPage(this string html){
            rawHtml = html;
            var title = GetFirstContent("<title>", "</title>");
            var links = GetAllContent("<a href=\"", "</a>");
            return new PageData(title, links);
        }
        static string GetFirstContent(string startTag, string endTag){
            if (TryGetIndexOf(startTag, 0, out var firstIndex))
                return string.Empty;
            firstIndex += startTag.Length;
            
            if (TryGetIndexOf(endTag, firstIndex, out var lastIndex))
                return string.Empty;
            
            var link = rawHtml.Substring(firstIndex, lastIndex - firstIndex);
            rawHtml = rawHtml.Remove(0, lastIndex);
            return link;
        }
        static bool TryGetIndexOf(string tag, int startIndex, out int resultIndex){
            resultIndex = rawHtml.IndexOf(tag, startIndex, StringComparison.OrdinalIgnoreCase);
            return resultIndex <= -1;
        }
        static List<ILink> GetAllContent(string startTag, string endTag){
            var subPages = new List<ILink>();
            while (true){
                var link = GetFirstContent(startTag, endTag);
                if(link == string.Empty)
                    break;
                if(!link.IsSubPage()) 
                    continue;
                subPages.Add(link.ConvertToSubPage());
            }
            return subPages;
        }
        class PageData : IPage{
            public string Uri{ get; }
            public string Info{ get; }
            public List<ILink> HyperLinks{ get; }
            public Dictionary<string, Pages> SubPageDictionary{ get; }

            public PageData(string description, List<ILink> links){
                Info = description;
                HyperLinks = links;
            }
        }
    }
}