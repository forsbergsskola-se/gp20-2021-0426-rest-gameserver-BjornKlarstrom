using System.Collections;
using System.Collections.Generic;

namespace GitHubExplorer {
    public class GithubUser {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public int Public_Repos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        
        public GithubUserInfo Info => new GithubUserInfo(this);
    }
    
    public class GithubUserInfo : GithubUser, IEnumerable, IEnumerator{
        int current = -1;
        readonly List<string> infos = new List<string>();
        readonly GithubUser response;
        public GithubUserInfo(GithubUser response) {
            this.response = response;
        }

        public IEnumerator GetEnumerator() {
            infos.Add(!string.IsNullOrEmpty(response.Name)
                ? $"Name: {response.Name}"
                : "Name not found / User don't exist.");

            infos.Add(!string.IsNullOrEmpty(response.Company)
                ? $"Company: {response.Company}"
                : "Company: unknown");

            infos.Add(!string.IsNullOrEmpty(response.Blog)
                ? $"Blog: {response.Blog}" 
                : "Blog: user has no blog.");

            infos.Add(
                !string.IsNullOrEmpty(response.Location)
                    ? $"Location: {response.Location}" 
                    : "Location: unknown.");

            infos.Add(!string.IsNullOrEmpty(response.Email)
                ? $"Email: {response.Email}" 
                : "Email: unknown");

            infos.Add(!string.IsNullOrEmpty(response.Public_Repos.ToString())
                ? $"Repos: {response.Public_Repos}"
                : "Repos: unknown");

            infos.Add(!string.IsNullOrEmpty(response.Followers.ToString())
                ? $"Followers: {response.Followers.ToString()}"
                : "Followers: unknown");

            infos.Add(!string.IsNullOrEmpty(response.Following.ToString())
                ? $"Following: {response.Following}"
                : "Following: unknown");

            infos.Add(
                !string.IsNullOrEmpty(response.Created_At)
                    ? $"Joined: {response.Created_At}" 
                    : "Joined: unknown.");

            infos.Add(!string.IsNullOrEmpty(response.Updated_At)
                ? $"Last updated: {response.Updated_At}"
                : "Last updated: unknown.");

            return this;
        }

        public bool MoveNext() {
            current++;
            return current < infos.Count;
        }

        public void Reset() {
            current = 0;
        }

        public object Current => infos[current];
    }
}