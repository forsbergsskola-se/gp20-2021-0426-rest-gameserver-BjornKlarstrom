using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace TinyBrowser
{
    internal static class TinyBrowser{
        static TcpClient tcpClient;
        static NetworkStream stream;
        static StreamReader streamReader;
        static StreamWriter streamWriter;

        const string hostUrl = "www.acme.com";
        const int tcpPort = 80;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tiny Browser!");
                
            // Setup client and get stream
            SetupTcpClient();
                
            // Send a HTTP-request over TCP (Root page/)
            SendGetRequest();
                
            // Get the response
            var response = GetResponseFromWebSite();
            Console.WriteLine(response);
            
            

            CloseApplication();
            
            
            
            
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

        static void SetupTcpClient(){
            // Connect to "acme.com" with TCP 
            tcpClient = new TcpClient(hostUrl, tcpPort);
            stream = tcpClient.GetStream();
            Console.WriteLine($"Connected to: {stream.Socket.RemoteEndPoint}");
            
            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);
        }
        
        static void SendGetRequest() {
            // Send HTTP-Request to the Stream
            var request = "GET / HTTP/1.1\r\n";
            request += "Host:"+ hostUrl +"\r\n";
            request += "\r\n";
            stream.Write(Encoding.ASCII.GetBytes(request));
        }
        
        static string GetResponseFromWebSite() {
            if(stream.CanRead){
                var response = streamReader.ReadToEnd();
                return response;
            }
            Console.WriteLine("Can't read from stream...");
            return string.Empty;
        }
        
        static void CloseApplication(){
            Console.WriteLine("Closing Application...");
            tcpClient.Close();
            stream.Close();
        }
    }
}
