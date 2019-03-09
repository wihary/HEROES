#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Shared
{
    using System;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel;

    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Class of navigation component.</summary>
    public class NavMenuBase : BlazorComponent
    {
        /// <summary>A value indicating whether the navigation menu is collapse.</summary>
        private protected bool collapseNavMenu = true;

        /// <summary>Gets or sets the application state.</summary>
        [Inject]
        protected AppState AppState { get; set; }

        /// <summary>
        ///     Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree. Override this method if you will perform
        ///     an asynchronous operation and want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            await this.AppState.IsLoggedInAsync();
            this.AppState.UserHasLoggedIn += this.UserLoggedIn;
            this.AppState.UserHasLoggedOut += this.UserLoggedOut;
        }

        /// <summary>Method to toggle the visibility of navigation menu.</summary>
        protected void ToggleNavMenu()
            => this.collapseNavMenu = !this.collapseNavMenu;

        private void UserLoggedIn(object sender, EventArgs e)
            => this.StateHasChanged();

        private void UserLoggedOut(object sender, EventArgs e)
            => this.StateHasChanged();
    }
}
#pragma warning restore SA1401 // Fields should be private
