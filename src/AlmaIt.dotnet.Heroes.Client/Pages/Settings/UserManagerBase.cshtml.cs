namespace AlmaIt.dotnet.Heroes.Client.Pages.Settings
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Components;

    public class UserManagerBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

    }
}