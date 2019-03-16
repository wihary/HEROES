namespace AlmaIt.Dotnet.Heroes.Client
{
    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using Blazor.Extensions.Storage;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Startup class of client.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Dependency injection configure method.
        /// </summary>
        /// <param name="services">Collection of services for dependency injection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddSingleton<AppState>();
        }

        /// <summary>
        /// Middleware configure method.
        /// </summary>
        /// <param name="app">Blazor application builder.</param>
        public void Configure(IBlazorApplicationBuilder app)
            => app.AddComponent<App>("app");
    }
}
