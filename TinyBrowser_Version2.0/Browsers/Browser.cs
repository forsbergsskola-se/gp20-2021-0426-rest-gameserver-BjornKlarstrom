using System;
using System.Collections.Generic;
using System.Linq;
using TinyBrowser.Helpers;
using TinyBrowser.Interfaces;

namespace TinyBrowser.Browsers{
    public abstract class Browser : IBrowser{
        
        IPage homePage;
        readonly List<IPage> pageHistory = new List<IPage>();
        int currentIndex;
        
        public int WebPageHtmlCount => pageHistory[currentIndex].HyperLinks.Count;
        public string GetCurrentWebPageUri => pageHistory[currentIndex].Uri;

        public bool CanReceiveWebPage(string host, string uri, int port){
            try{
                var rawHtml = GetWebPageHtml(host, uri, port);
                homePage = rawHtml.ConvertHtmlToWebPage();
                uri = uri == string.Empty ? host : $"{host}/{uri}";
                homePage = Pages.SortPages(homePage, uri);
                pageHistory.Add(homePage);
                currentIndex = pageHistory.IndexOf(homePage);
                return true;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected virtual string GetWebPageHtml(string host,string uri, int port){
            throw new NotImplementedException();
        }
        public bool CanGoBack(){
            if(currentIndex <= 0)
                return false;
            currentIndex--;
            Console.WriteLine(pageHistory[currentIndex].Uri);
            return true;
        }
        public bool CanGoForward(){
            if(currentIndex >= pageHistory.Count-1)
                return false;
            currentIndex++;
            Console.WriteLine(pageHistory[currentIndex].Uri);
            return true;
        }
        public void CanGoToSubPage(string uri){
            if (!pageHistory[currentIndex].SubPageDictionary.ContainsKey(uri)) return;
            if(currentIndex < pageHistory.Count-1)
                pageHistory.RemoveRange(currentIndex+1,pageHistory.Count-currentIndex-1);
            pageHistory.Add(pageHistory[currentIndex].SubPageDictionary[uri]);
            currentIndex++;
        }
        public void TryGoToHtmlIndex(int index, int port){
            var uri = pageHistory[currentIndex].HyperLinks[index].Uri;

            if (!pageHistory[currentIndex].Uri.Contains(".html")){
                CanReceiveWebPage(pageHistory[currentIndex].Uri, uri, port);
                return;
            } 
                
            
            var lastIndexOf = pageHistory[currentIndex].Uri.LastIndexOf("/", StringComparison.Ordinal);
            if (lastIndexOf <= -1)
                return;
            var noHtml = pageHistory[currentIndex].Uri.Substring(0, lastIndexOf);
            CanReceiveWebPage(noHtml, uri, port);
        }

        public void GetSearchHistory(){
            Console.WriteLine("Search history: ");
            for (var i = 0; i < pageHistory.Count; i++){
                var indicator = i == currentIndex ? ">" : string.Empty;
                Console.WriteLine($"{indicator}({i}){pageHistory[i].Uri} {pageHistory[i].Info}");
            }
        }
        public string[] GetCurrentWebPage(){
            return pageHistory[currentIndex].SubPageDictionary.Keys.ToArray();
        }
        public void DisplaySubPages(){
            var temp = pageHistory[currentIndex];
            var index = 0;
            var webPages = "Pages: \n";
            foreach (var (pathName, webPage) in temp.SubPageDictionary){
                webPages += $"({index}) {pathName}/ {webPage.Title.TryShorten()}\n";
                index++;
            }
            Console.WriteLine(webPages);
        }
        public void DisplayHyperLinks(){
            var temp = pageHistory[currentIndex];
            var hyperLinks = $"{temp.Info} {temp.Uri}\nHtml Pages:\n";
            for (var i = 0; i < temp.HyperLinks.Count; i++){
                hyperLinks += $"({i}) {temp.HyperLinks[i].Uri} {temp.HyperLinks[i].Info}\n";
            }
            Console.WriteLine(hyperLinks);
        }
    }
}