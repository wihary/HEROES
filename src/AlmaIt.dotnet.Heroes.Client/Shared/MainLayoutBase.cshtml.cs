namespace AlmaIt.Dotnet.Heroes.Client.Shared
{
    using System;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Layouts;

    public class MainLayoutBase : BlazorLayoutComponent
    {
        [Inject]
        public AppState AppState { get; set; }

        protected override async Task OnInitAsync()
        {
            await this.AppState.IsLoggedInAsync();
        }
    }
}