using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace GitHubExplorer
{
    class Program
    {
        public class Secrets{
            public string Token{ get; set; }
        }

        static Secrets LoadAndValidateSecrets(){
            Secrets secrets;
            if (!File.Exists("secrets.json")){
                secrets = new Secrets();
                File.WriteAllText("secrets.json", JsonSerializer.Serialize(secrets));
            }
            else{
                secrets = JsonSerializer.Deserialize<Secrets>(File.ReadAllText("secrets.json"));
            }

            if (!string.IsNullOrEmpty(secrets?.Token)) return secrets;
            throw new Exception($@"ERROR: Needs to define a token in file {Path.Combine(System.Environment 
                .CurrentDirectory, "secrets.json")}");
        }
        
        static void Main(string[] args){

            var secrets = LoadAndValidateSecrets();
            
            Console.WriteLine("Welcome to the GitHub Browser!");

            Console.WriteLine("Select a user you want to inspect.");

            //var userToInspect = Console.ReadLine();
            var repo = "https://github.com/forsbergsskola-se/gp20-2021-0426-rest-gameserver-BjornKlarstrom";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "token : " + secrets.Token);
            
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, repo);
            httpClient.Send(httpRequestMessage);

            Console.WriteLine(httpRequestMessage.ToString());
        }
    }
}
