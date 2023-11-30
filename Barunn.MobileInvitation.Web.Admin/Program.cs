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
                        //운영 환경에서는 KeyVualt 사용
                        builder.AddAzureKeyVault(
                            new Uri($"https://barunsecret.vault.azure.net/"),
                            new DefaultAzureCredential());
                    }
                    else if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        //개발용 환경에서만 사용
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
