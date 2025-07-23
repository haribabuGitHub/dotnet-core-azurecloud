using Azurekeyvault.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        KeyVaultSecretService  azurekeyvault = new KeyVaultSecretService();
        Console.WriteLine("secrete key "+ azurekeyvault.GetSecreteByKey());
    }
}