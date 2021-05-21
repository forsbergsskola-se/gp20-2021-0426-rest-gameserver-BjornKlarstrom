namespace TinyBrowser.Interfaces{
    public interface IBrowser{
        int WebPageHtmlCount{ get; }
        string GetCurrentWebPageUri{ get; }
        bool CanReceiveWebPage(string host, string uri, int port);
        string[] GetCurrentWebPage();
        bool CanGoBack();
        bool CanGoForward();
        void CanGoToSubPage(string uri);
        void TryGoToHtmlIndex(int index, int port);
        void GetSearchHistory();
        void DisplaySubPages();
        void DisplayHyperLinks();
    }
}