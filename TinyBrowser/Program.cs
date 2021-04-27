using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    internal static class TinyBrowser
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tiny Browser!");
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("GET / HTTP/0.9");
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

            var data = memory.ToArray();
            Console.WriteLine(Encoding.ASCII.GetString(data,0,data.Length));
            
        }
    }
}
