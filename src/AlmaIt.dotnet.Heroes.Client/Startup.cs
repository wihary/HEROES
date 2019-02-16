namespace AlmaIt.dotnet.Heroes.Client
{
    using AlmaIt.dotnet.Heroes.Client.Helpers;

    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {

        public void Configure(IBlazorApplicationBuilder app)
            => app.AddComponent<App>("app");

        public void ConfigureServices(IServiceCollection services)
            => services.AddTransient<HtmlHelper>();
    }
}
