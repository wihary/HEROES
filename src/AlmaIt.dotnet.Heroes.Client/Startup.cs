namespace AlmaIt.dotnet.Heroes.Client
{
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Start up class of client.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Middleware configure method.
        /// </summary>
        /// <param name="app">Blazor application builder.</param>
        public void Configure(IBlazorApplicationBuilder app)
            => app.AddComponent<App>("app");

        /// <summary>
        /// Dependency injection configure method.
        /// </summary>
        /// <param name="services">Collection of services for dependency injection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
