namespace AlmaIt.dotnet.Heroes.Client
{
    using Microsoft.AspNetCore.Blazor.Hosting;

    /// <summary>Entry point class.</summary>
    public static class Program
    {
        /// <summary>Entry point of program.</summary>
        public static void Main()
            => CreateHostBuilder().Build().Run();

        /// <summary>Method of blazor host builder.</summary>
        /// <returns>Return the web builder.</returns>
        private static IWebAssemblyHostBuilder CreateHostBuilder() =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                                 .UseBlazorStartup<Startup>();
    }
}
