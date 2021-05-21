using System;
using System.IO;

namespace TinyBrowser.Browsers{
    public class LocalFileBrowser : Browser{
        
        static readonly string path = Environment.CurrentDirectory;
        protected override string GetWebPageHtml(string host,string uri, int port){
            return File.ReadAllText($"{path}/{host}.Html");
        }
    }
}