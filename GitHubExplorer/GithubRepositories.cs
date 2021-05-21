using System.Collections.Generic;
using System.Text.Json;

namespace GitHubExplorer {
    public class GithubRepositories {
        
        public readonly List<string> names;
        public readonly List<string> urls;
        public readonly List<string> descriptions;

        public GithubRepositories(JsonElement deserializedJson) {
            
            names = new List<string>();
            urls = new List<string>();
            descriptions = new List<string>();
            
            foreach (var element in deserializedJson.EnumerateArray()) {
                names.Add(element.GetProperty("name").GetString());
                urls.Add(element.GetProperty("url").GetString());
                descriptions.Add(element.GetProperty("description").GetString());
            }
        }
    }
}