using AntiCorruptionLayer.Adapter;
using AntiCorruptionLayer.Api.Controllers;
using AntiCorruptionLayer.Domain;
using AntiCorruptionLayer.Domain.Helpers;
using AntiCorruptionLayer.Domain.Interfaces;
using AntiCorruptionLayer.Facade;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AntiCorruptionLayer.Test
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.Development.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();

            hostBuilder.ConfigureHostConfiguration(builder =>
            {
                builder.AddConfiguration(config);
            });
        }

        public void ConfigureServices(IServiceCollection services, HostBuilderContext builder)
        {
            services.AddHttpClient();

            Config.GitHubBaseUrl = builder.Configuration.GetValue<string>("GitHubBaseUrl");
            Config.GitHubBearer = builder.Configuration.GetValue<string>("GitHubBearer");
            Config.GitHubOwner = builder.Configuration.GetValue<string>("GitHubOwner");

            services.AddTransient<IHTTPRequestGeneric, HTTPRequestGeneric>();
            services.AddTransient<GitHubController>();
            services.AddTransient<IGitHubFacade, GitHubFacade>();
            services.AddTransient<IGitHubAdapter, GitHubAdapter>();
        }
    }
}