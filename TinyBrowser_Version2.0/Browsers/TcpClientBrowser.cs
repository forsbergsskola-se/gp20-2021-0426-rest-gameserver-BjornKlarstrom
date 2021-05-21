using System;
using System.IO;
using System.Net.Sockets;

namespace TinyBrowser.Browsers{
    public class TcpClientBrowser : Browser{
        protected override string GetWebPageHtml(string host, string uri, int port){
            
            var tcpClient = new TcpClient(host, port);
                var stream = tcpClient.GetStream();
                var streamWriter = new StreamWriter(stream){
                    AutoFlush = true
                };
                uri = uri != string.Empty ? new Uri($"http://www.{host}/{uri}").AbsoluteUri : "/";
                
                streamWriter.Write($"GET {uri} HTTP/1.1\r\nHost: {host}\r\n\r\n");
                var streamReader = new StreamReader(stream);
                var rawHtml = streamReader.ReadToEnd();
                tcpClient.Close();
                return rawHtml;
        }
    }
}