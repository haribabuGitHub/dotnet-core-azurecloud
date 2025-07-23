using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVault.Services
{
    public interface IKeyVaultSecretService
    {
        string GetSecreteByKey();
    }

    public class KeyVaultSecretService : IKeyVaultSecretService
    {
        public KeyVaultSecretService()
        {
        }

        public string GetSecreteByKey()
        {
          /*  string TanentId = Environment.GetEnvironmentVariable("1d973eb9-4932-4edd-877a-dd1fd9b8ffcd") ?? throw new NullReferenceException();
            string ApplicationIdUrl = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING") ?? throw new NullReferenceException();
            string ApplicationId = ApplicationIdUrl.Split("=")[^1];
            string VaultUrl = "https://zuriskeyvault.vault.azure.net/";
            string SecreteKey = SfConnectionString.Split('/')[^1];
            var secretClient = new SecretClient(new Uri(VaultUrl), new ClientSecretCredential(TanentId, ApplicationId, SecreteKey));
            return secretClient.GetSecret("secretName").Value.Value;*/

            SecretClientOptions options = new ()
            {
                Retry =
            {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
            }
            };
            var client = new SecretClient(new Uri("https://zuriskeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret("k20");
            string secretValue = secret.Value;
            return secretValue;
        }
    }
}