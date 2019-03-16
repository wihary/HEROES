namespace AlmaIt.Dotnet.Heroes.Client.Pages
{
    using System;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;

    using Blazor.Extensions.Storage;

    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>
    /// Class of index page.
    /// </summary>
    public class IndexBase : BlazorComponent
    {
        /// <summary>
        /// Gets the message of alert.
        /// </summary>
        private protected string Message { get; private set; }

        /// <summary>
        /// Gets the type of alert.
        /// </summary>
        private protected AlertType Type { get; private set; }

        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private SessionStorage SessionStorage { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            this.AppState.UserHasLoggedOut += async (s, e) => await this.UserLoggedOutAsync(s, e).ConfigureAwait(false);

            await this.DisplayAvailableMessageAsync();
        }

        private async Task DisplayAvailableMessageAsync()
        {
            var message = await this.SessionStorage.GetItem<string>("message");

            if (!string.IsNullOrEmpty(message))
            {
                var messageType = (AlertType)Enum.Parse(typeof(AlertType), await this.SessionStorage.GetItem<string>("messageType"));
                await this.SessionStorage.RemoveItem("message");
                await this.SessionStorage.RemoveItem("messageType");
                this.Message = message;
                this.Type = messageType;
            }
        }

        private async Task UserLoggedOutAsync(object sender, EventArgs e)
        {
            await this.DisplayAvailableMessageAsync().ConfigureAwait(false);
            this.StateHasChanged();
        }
    }
}
