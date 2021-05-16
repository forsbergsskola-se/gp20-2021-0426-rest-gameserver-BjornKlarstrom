using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestClient{
    public abstract class RestBase{
        protected Uri uri;

        protected static async Task<string> Request(Uri uri, HttpMethod httpMethod, object obj){
            
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MMORPG/v1");
            
            var requestMessage = new HttpRequestMessage{
                RequestUri = uri, 
                Method = httpMethod
            };

            if (httpMethod == HttpMethod.Post){
                // Testa om sista funkar
                var stringContent = new StringContent(
                    JsonSerializer.Serialize(obj), Encoding.UTF8,MediaTypeNames.Application.Json);
                await httpClient.PostAsync(uri, stringContent);
            }

            if (httpMethod == HttpMethod.Get) await httpClient.SendAsync(requestMessage);

            var requestData = string.Empty;

            if (requestMessage.Content != null){
                var contentStream = await requestMessage.Content.ReadAsStreamAsync();
                var streamReader = new StreamReader(contentStream);
                requestData = await streamReader.ReadToEndAsync();
            }
            
            httpClient.Dispose();
            return requestData;
        }
    }
}