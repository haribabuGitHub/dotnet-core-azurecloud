using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVault.Services
{
    public interface IKeyVaultSecretService
    {
        Task<Azure.Response<KeyVaultSecret>> GetSecreteByKey();
    }

    public class KeyVaultSecretService : IKeyVaultSecretService
    {
        public KeyVaultSecretService()
        {
        }

        public async Task<Azure.Response<KeyVaultSecret>> GetSecreteByKey()
        {
            /*  string TanentId = Environment.GetEnvironmentVariable("1d973eb9-4932-4edd-877a-dd1fd9b8ffcd") ?? throw new NullReferenceException();
              string ApplicationIdUrl = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING") ?? throw new NullReferenceException();
              string ApplicationId = ApplicationIdUrl.Split("=")[^1];
              string VaultUrl = "https://zuriskeyvault.vault.azure.net/";
              string SecreteKey = SfConnectionString.Split('/')[^1];
              var secretClient = new SecretClient(new Uri(VaultUrl), new ClientSecretCredential(TanentId, ApplicationId, SecreteKey));
              return secretClient.GetSecret("secretName").Value.Value;*/


            const string secretName = "mySecret";
            //var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var keyVaultName = "zuriskeyvault";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            Console.Write("Input the value of your secret > ");
            var secretValue = Console.ReadLine();

            Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
            await client.SetSecretAsync(secretName, secretValue);
            Console.WriteLine(" done.");

            Console.WriteLine("Forgetting your secret.");
            secretValue = string.Empty;
            Console.WriteLine($"Your secret is '{secretValue}'.");

            Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

            Console.Write($"Deleting your secret from {keyVaultName} ...");
            DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
            // You only need to wait for completion if you want to purge or recover the secret.
            await operation.WaitForCompletionAsync();
            Console.WriteLine(" done.");

            Console.Write($"Purging your secret from {keyVaultName} ...");
            await client.PurgeDeletedSecretAsync(secretName);
            Console.WriteLine(" done.");

            //SecretClientOptions options = new ()
            //{
            //    Retry =
            //{
            //Delay= TimeSpan.FromSeconds(2),
            //MaxDelay = TimeSpan.FromSeconds(16),
            //MaxRetries = 5,
            //Mode = RetryMode.Exponential
            //}
            //};
            //var client = new SecretClient(new Uri("https://zuriskeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);
            //KeyVaultSecret secret = client.GetSecret("k20");
            //string secretValue = secret.Value;
            // return secretValue;
            return secret;
        }
    }
}