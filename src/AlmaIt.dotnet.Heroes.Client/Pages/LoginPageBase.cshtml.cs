#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Pages
{
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;

    using Blazor.Extensions.Storage;

    using global::Dotnet.JsonIdentityProvider.IdentityProvider.Model;

    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;

    /// <summary>Component represent the login page.</summary>
    public class LoginPageBase : BlazorComponent
    {
        /// <summary>Gets or sets the user to log.</summary>
        private protected CredentialModel User { get; set; } = new CredentialModel();

        /// <summary>Gets the login message.</summary>
        private protected string LoginMessage { get; private set; }

        /// <summary>Gets or sets a value indicating whether the login is successful.</summary>
        private protected bool IsLoginSucess { get; set; }

        /// <summary>Gets the type of alert.</summary>
        private protected AlertType GetAlertType => this.IsLoginSucess ? AlertType.Success : AlertType.Danger;

        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private SessionStorage SessionStorage { get; set; }

        /// <summary>
        /// Method to sign-in User.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task SignIn()
        {
            var (success, message) = await this.AppState.LoginAsync(this.User);

            this.LoginMessage = message;
            this.IsLoginSucess = success;

            if (this.IsLoginSucess)
            {
                await this.SessionStorage.SetItem("message", this.LoginMessage);
                await this.SessionStorage.SetItem("messageType", this.GetAlertType.ToString());
                this.UriHelper.NavigateTo("/home");
            }
            else
            {
                this.StateHasChanged();
            }
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
