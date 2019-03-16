namespace AlmaIt.Dotnet.Heroes.Server
{
    using AlmaIt.Dotnet.Heroes.Server.Data;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>Class entry point of application.</summary>
    public static class Program
    {
        /// <summary>Entry point of application.</summary>
        /// <param name="args">Arguments of application.</param>
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

        /// <summary>WebHost creation method.</summary>
        /// <param name="args">command line arguments.</param>
        /// <returns>Returns the webHost.</returns>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseConfiguration(new ConfigurationBuilder()
                                     .AddCommandLine(args)
                                     .Build())
                   .UseStartup<Startup>();
    }
}
