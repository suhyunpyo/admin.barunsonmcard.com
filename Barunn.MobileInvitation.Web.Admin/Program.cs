using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Barunn.MobileInvitation.Web.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    if (hostContext.HostingEnvironment.IsProduction())
                    {
                        //� ȯ�濡���� KeyVualt ���
                        builder.AddAzureKeyVault(
                            new Uri($"https://barunsecret.vault.azure.net/"),
                            new DefaultAzureCredential());
                    }
                    else if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        //���߿� ȯ�濡���� ���
                        builder.AddAzureKeyVault(
                            new Uri($"https://dev-barunsecret.vault.azure.net/"),
                            new DefaultAzureCredential());
                    }
                    else if (hostContext.HostingEnvironment.IsEnvironment("Local"))
                    {
                        builder.AddUserSecrets<Program>();
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
