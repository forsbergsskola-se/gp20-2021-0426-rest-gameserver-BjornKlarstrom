using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestClient{
    
    public abstract class Rest{
        protected Uri uri;

        protected static Task<string> Request(Uri uri, HttpMethod httpMethod, object obj){
            
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("TestClient/v1");

            return Task.FromResult("");
        }
    }
}