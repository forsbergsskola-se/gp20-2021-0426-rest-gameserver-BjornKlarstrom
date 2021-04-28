using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace TinyBrowser
{
    internal static class TinyBrowser
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tiny Browser!");
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("GET / HTTP/1.1");
            stringBuilder.AppendLine("Host: www.acme.com");
            stringBuilder.AppendLine("Connection: close");
            stringBuilder.AppendLine();
            
            Console.WriteLine(stringBuilder.ToString());
            
            
            var tcpClient = new TcpClient("acme.com",80);

            var stream = tcpClient.GetStream();

            var message = Encoding.ASCII.GetBytes(stringBuilder.ToString());
            
            stream.Write(message,0,message.Length);
            

            // Receive
            var memory = new MemoryStream();
            stream.CopyTo(memory);
            memory.Position = 0;
            var dataResponse = memory.ToArray();
            var response = Encoding.ASCII.GetString(dataResponse, 0, dataResponse.Length);

            Console.WriteLine(response);
            
            // Searches
            string SearchForTitle(){
                const string openElement = "<title>";
                const string closeElement = "</title>";
                
                var startIndex = response.IndexOf(openElement, StringComparison.Ordinal);
                startIndex += openElement.Length;
                
                var endIndex = response.IndexOf(closeElement, StringComparison.Ordinal);

                return response.Substring(startIndex, (endIndex - startIndex));
            }
            Console.WriteLine(SearchForTitle());

            var occurrences = GetAllIndexesOfTag("<a href=\"https:", response);

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
            }
            
        }
    }
}
