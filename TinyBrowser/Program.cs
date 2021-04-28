using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;


namespace TinyBrowser
{
    internal static class TinyBrowser
    {
        /*public struct HostAndUrl{
            public string hostName;
            public string url;
        }
        
        interface IVisitUrlStrategy{
            bool CanHandle(string url);
            HostAndUrl GetUrlAndHostnameFor(HostAndUrl current, string url);
        }
        
        public class VisitLocalUrlStrategy : IVisitUrlStrategy{
            public bool CanHandle(string url){
                return url.StartsWith("/");
            }

            public HostAndUrl GetUrlAndHostnameFor(HostAndUrl current, string url){
                return new HostAndUrl{
                    hostName = current.hostName,
                    url = Path.Combine(current.url, url)
                };
            }

        }*/

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tiny Browser!");
 
            // Connect to acme.com with TCP 
            var tcpClient = new TcpClient("acme.com",80);
            var stream = tcpClient.GetStream();
            
            // Send a HTTP-request over TCP (Root page/)
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write("GET / HTTP/1.1\r\nHost: acme.com\r\n\r\n");
            
            // Read the response
            var streamReader = new StreamReader(stream);
            var response = streamReader.ReadToEnd();

            Console.WriteLine(response);
            
            
            //Console.WriteLine(SearchForTitle());
            // Searches
            /*string SearchForTitle(){
                const string openElement = "<title>";
                const string closeElement = "</title>";
                
                var startIndex = response.IndexOf(openElement, StringComparison.Ordinal);
                startIndex += openElement.Length;
                
                var endIndex = response.IndexOf(closeElement, StringComparison.Ordinal);

                return response.Substring(startIndex, (endIndex - startIndex));
            }*/

            /*var occurrences = GetAllIndexesOfTag("<a href=\"https:", response);

            foreach (var tag in occurrences){
                Console.WriteLine(response.Substring(tag,28));
            }

            static IEnumerable<int> GetAllIndexesOfTag(string value, string response) { 
                var indexesOfValue = new List<int>();
                
                for (var index = 0;; index += value.Length) {
                    index = response.IndexOf(value, index, StringComparison.Ordinal);
                    if (index <= 0)
                        return indexesOfValue;
                    indexesOfValue.Add(index);
                }
            }*/
        }
    }
}
