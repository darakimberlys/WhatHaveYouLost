using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.IOC;

public static class SettingsCollection
{

    public static void AddSecretVault(this IServiceCollection services)
    {
        var credential = new DefaultAzureCredential();

        var client =
            new SecretClient(
                new Uri("https://secretsfor.vault.azure.net"), credential);
        
        var secret = client.GetSecret("SQLCONNECTION");    
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(secret.Value.ToString());
        });
    }
}