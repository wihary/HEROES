namespace AlmaIt.dotnet.Heroes.Client
{
    #region Usings

    using AlmaIt.dotnet.Heroes.Client.Helpers;

    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public class Startup
    {

        public void Configure(IBlazorApplicationBuilder app)
            => app.AddComponent<App>("app");

        public void ConfigureServices(IServiceCollection services)
            => services.AddTransient<HtmlHelper>();
    }
}
