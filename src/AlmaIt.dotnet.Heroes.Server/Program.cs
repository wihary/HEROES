using AlmaIt.dotnet.Heroes.Server.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlmaIt.dotnet.Heroes.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                provider.InitData();
            }

            host.Run();
        }

        /// <summary>
        ///     WebHost creation method
        /// </summary>
        /// <param name="args">command line arguments</param>
        /// <returns>Returns the webHost</returns>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>();
    }
}
