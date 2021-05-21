using System;
using System.IO;
using System.Net;

namespace TinyBrowser.Browsers{
    public class HttpRequestBrowser : Browser{
        protected override string GetWebPageHtml(string host, string uri, int port){
            
            uri = uri != string.Empty ? new Uri($"http://www.{host}/{uri}").AbsoluteUri : $"http://{host}";
            try{
                var request = WebRequest.Create(uri) as HttpWebRequest;
                Console.WriteLine(uri);
                var response = request?.GetResponse();
                var receiveStream = response?.GetResponseStream();
                var readStream = new StreamReader(receiveStream!);
                var result = readStream.ReadToEnd();
                return result;
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}