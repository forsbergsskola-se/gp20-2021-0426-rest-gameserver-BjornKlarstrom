using System;
using System.IO;
using System.Text.Json;

namespace GitHubExplorer.Secrets {
    public static class SecretValidator {
        public static string LoadToken() {
            
            GitHubExplorer.Secrets.Secrets secrets;
            
            if (!File.Exists("secrets.json")) {
                secrets = new GitHubExplorer.Secrets.Secrets();
                File.WriteAllText("secrets.json", JsonSerializer.Serialize(secrets));
            }
            else {
                secrets = JsonSerializer.Deserialize<GitHubExplorer.Secrets.Secrets>(File.ReadAllText("secrets.json"));
            }

            if (string.IsNullOrEmpty(secrets?.Token)) {
                throw new Exception("Error: No Token in 'secrets.json'-file.");
            }
            
            return secrets.Token;
        }
    }
}