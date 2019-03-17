namespace AlmaIt.Dotnet.Heroes.Client.Shared
{
    using System;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel;

    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Class represent the shared login info component.</summary>
    public class LoginInfoBase : BlazorComponent
    {
        /// <summary>Gets or sets the application state.</summary>
        [Inject]
        private protected AppState AppState { get; set; }

        /// <summary>
        /// Gets the username connected.
        /// </summary>
        private protected string UserName { get; private set; }

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
            this.UserName = await this.AppState.GetConnectedUserName();
        }

        private async void UserLoggedIn(object sender, EventArgs e)
        {
            this.UserName = await this.AppState.GetConnectedUserName();
            this.StateHasChanged();
        }

        private void UserLoggedOut(object sender, EventArgs e)
        {
            this.UserName = string.Empty;
            this.StateHasChanged();
        }
    }
}
