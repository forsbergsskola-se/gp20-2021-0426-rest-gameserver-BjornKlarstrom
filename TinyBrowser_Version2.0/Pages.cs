using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Interfaces;

namespace TinyBrowser{
    public class Pages : IPage{
        public string Title{ get; }
        public string Uri{ get; }
        public string Info{ get; }
        public List<ILink> HyperLinks{ get; set; }
        public Dictionary<string, Pages> SubPageDictionary{ get; set;}

        Pages(string title, string uri){
            Title = title;
            Uri = uri;
            SubPageDictionary = new Dictionary<string, Pages>();
            HyperLinks = new List<ILink>();
        }

        public static IPage SortPages(IPage page, string uri){
            var sortedWebPage = new Pages(page.Info, uri);
            foreach (var link in page.HyperLinks){
                
                var splitLink = link.Uri.Trim().Split("/");
                if (ContainsOnlyOneIndex(splitLink)){
                    if (IsHtmlLink(splitLink[0])){
                        sortedWebPage.HyperLinks.Add(link);
                    }
                    else{
                        sortedWebPage.SubPageDictionary.TryAdd(splitLink[0], 
                            new Pages(link.Info, sortedWebPage.Uri+ "/"+link.Uri));
                    }
                }
                else{
                    sortedWebPage.TryAdd(splitLink, link);
                }
            }
            return sortedWebPage;
        }

        void TryAdd(IReadOnlyList<string> uri, ILink link){
            if (uri[0] == "")
                return;
            if (IsHtmlLink(uri[0])){
                HyperLinks.Add(link);
                return;
            }
            if (!SubPageDictionary.ContainsKey(uri[0])){
                var webPageInstance = new Pages(link.Info, uri[0]);
                SubPageDictionary.Add(uri[0], webPageInstance);
                if (uri.Count > 0)
                    webPageInstance.TryAdd(uri.Skip(1).ToArray(), link);
                return;
            }
            if (uri.Count > 0){
                SubPageDictionary[uri[0]].TryAdd(uri.Skip(1).ToArray(), link);
            }
        }

        static bool ContainsOnlyOneIndex(IReadOnlyList<string> splitUri){
            return splitUri.Count == 1 || splitUri[1] == "";
        }
        static bool IsHtmlLink(string link){
            return link.EndsWith(".html");
        }
    }
}