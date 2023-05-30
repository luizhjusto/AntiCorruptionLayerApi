using AntiCorruptionLayer.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace AntiCorruptionLayer.Test
{
    public static class Program
    {
        public static void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder
                    .UseTestServer(options => options.PreserveExecutionContext = true)
                    .UseStartup<Startup>();
            });
        }
    }
}
