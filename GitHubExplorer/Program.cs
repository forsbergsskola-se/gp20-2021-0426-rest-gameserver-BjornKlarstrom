using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubExplorer.Secrets;

namespace GitHubExplorer
{
    internal static class Program
    {
        static void Main(string[] args){
            MainLoop();
        }

        static void MainLoop(){
            
            Console.Clear();

            Console.WriteLine("\n>>>>>>>>>> GitHub Explorer <<<<<<<<<<" +
                              "\n\nWhat would you like to search for?" +
                              "\n1. A github username" +
                              "\n2. A github username(and get all the repositories)");

            int.TryParse(Console.ReadLine(), out var input);
            
            switch (input) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\nType a github username");
                    Task.WaitAll(SearchForUser(Console.ReadLine()));
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\nType a github username and get all their repositories");
                    Task.WaitAll(SearchForRepositories(Console.ReadLine()));
                    break;
            }
        }

        static async Task SearchForRepositories(string user)
        {
            Console.Clear();
            var client = new HttpClient {BaseAddress = new Uri("https://api.github.com")};
            var token = SecretValidator.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync($"/users/{user}/repos");
            
            var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            
            var repoJson = await streamReader.ReadToEndAsync();
            streamReader.Close();
            client.Dispose();

            var reposDes  = JsonSerializer.Deserialize<JsonElement>(repoJson);
            var repos = new GithubRepositories(reposDes);

            for (var i = 0; i < repos.names.Count; i++) {
                Console.WriteLine("---------------------------");
                Console.WriteLine($"Name: {repos.names[i]}");
                Console.WriteLine($"Description: {repos.descriptions[i]}");
                Console.WriteLine($"Url: {repos.urls[i]}");
                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Press any key for main menu");
            Console.WriteLine("2. Go to user profile");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForUser(user));
            }
            else {
                MainLoop();
            }
        }
        
        static async Task SearchForUser(string userName)
        {
            Console.Clear();
            var client = new HttpClient {BaseAddress = new Uri("https://api.github.com")};
            var token = SecretValidator.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            
            var response = await client.GetAsync($"/users/{userName}");
            
            var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            
            var userData = await streamReader.ReadToEndAsync();
            streamReader.Close();
            client.Dispose();
            
            var user = JsonSerializer.Deserialize<GithubUser>(userData);

            if (user != null)
                foreach (var info in user.Info) {
                    Console.WriteLine(info);
                }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Press any key for main menu");
            Console.WriteLine("2. Go to users repositories");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForRepositories(userName));
            }
            else {
                MainLoop();
            }
        }
    }
}
