namespace AlmaIt.Dotnet.Heroes.Client.Shared
{
    using System;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using Microsoft.AspNetCore.Blazor.Components;

    public class NavMenuBase : BlazorComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        protected bool collapseNavMenu = true;

        protected override async Task OnInitAsync()
        {
            await this.AppState.IsLoggedInAsync();
            this.AppState.UserHasLoggedIn += this.UserLoggedIn;
            this.AppState.UserHasLoggedOut += this.UserLoggedOut;
        }

        private void UserLoggedIn(object sender, EventArgs e)
        {
            this.StateHasChanged();
        }

        private void UserLoggedOut(object sender, EventArgs e)
        {
            this.StateHasChanged();
        }

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}