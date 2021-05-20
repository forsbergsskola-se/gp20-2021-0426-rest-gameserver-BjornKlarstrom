using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MMORPGTestClient{
    class Program{
        
        const string localUrl = "https://localhost:5001/api/mmorpg/Players";
        static void Main(string[] args){
            
            var players = new List<Player>();
            
            
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(localUrl);

            var result = response.Result;

            Console.WriteLine(result.ToString());

            httpClient.Dispose();
        }
    }
}