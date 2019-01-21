namespace AlmaIt.dotnet.Heroes.Client.Shared
{
    using System;
    using AlmaIt.dotnet.Heroes.Client.ViewModel;
    using Microsoft.AspNetCore.Blazor.Components;

    public class NavMenuBase : BlazorComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        protected bool collapseNavMenu = true;

        protected override void OnInit()
        {
            this.AppState.UserHasLoggedIn += this.UserLoggedIn;
        }

        private void UserLoggedIn(object sender, EventArgs e)
        {
            Console.WriteLine($"event called, logged = {this.AppState.IsLoggedin}");

            this.StateHasChanged();
        }

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}