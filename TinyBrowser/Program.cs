using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Console.WriteLine(SearchForTitle(response));

            // Get and Print Links
            var links = FindAllLinksWithTitles(response).ToArray();
            for (var i = 0; i < links.Length; i++){
                Console.WriteLine($"{i}: {links[i].displayText} ({links[i].urlLink})");
            }
            
            // Ask user for input
            Console.WriteLine($"\nCHOOSE ONE OF THE LINKS: (0 - {links.Length})");
            Console.Read();

            CloseApplication();
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

            streamWriter.AutoFlush = true;
            streamWriter.Write(request);
        }
        
        static string GetResponseFromWebSite() {
            if(stream.CanRead){
                var response = streamReader.ReadToEnd();
                return response;
            }
            Console.WriteLine("Can't read from stream...");
            return string.Empty;
        }
        
        // Searches
        static string SearchForTitle(string str){
            const string openElement = "<title>";
            const string closeElement = "</title>";
                
            var startIndex = str.IndexOf(openElement, StringComparison.Ordinal);
            startIndex += openElement.Length;
                
            var endIndex = str.IndexOf(closeElement, StringComparison.Ordinal);

            return str.Substring(startIndex, (endIndex - startIndex));
        }
        
        // HTML Example...
        // <b>The <a href="build_a_pc/boardfinder/">ACME Motherboard Finder</a>.</b>
        static IEnumerable<LinkAndTitle> FindAllLinksWithTitles(string response) { 
                
            const string linkTag = "<a href=\"";
            const char quote = '"';
            const char titleStartTag = '>';
            const string titleEndTag = "</a>";
                
                
            var listOfLinks = new List<LinkAndTitle>();
                
            var splitArray = response.Split(linkTag);
            splitArray = splitArray.Skip(1).ToArray();

            foreach (var split in splitArray){
                var url = split.TakeWhile(symbol => symbol != quote).ToArray();
                var afterUrlPart = split[url.Length..];
                var displayTextStart = afterUrlPart.IndexOf(titleStartTag) + 1;
                var displayTextEnd = afterUrlPart.IndexOf(titleEndTag, StringComparison.Ordinal);
                var displayText = afterUrlPart.Substring(displayTextStart,(displayTextEnd - displayTextStart))
                    .Replace("<b>", string.Empty).Replace("</b>", string.Empty);

                if (displayText.StartsWith("<img")){
                    displayText = "Image";
                }
                listOfLinks.Add(new LinkAndTitle{
                    urlLink = new string(url),
                    displayText = new string(displayText)
                });
            }
            return listOfLinks;
        }
        
        static void CloseApplication(){
            Console.WriteLine("Closing Application...");
            tcpClient.Close();
            stream.Close();
        }
    }

    public class LinkAndTitle{
        public string urlLink;
        public string displayText;
    }
}
