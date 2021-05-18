using System;
using System.Net.Http;

namespace MMORPGTestClient{
    class Program{
        static void Main(string[] args){

            const string localUrl = "https://localhost:5001/";
            // Step.1
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync(localUrl);

            var result = response.Result;

            Console.WriteLine(result.ToString());

            httpClient.Dispose(); // Close and release resource
        }
    }
}