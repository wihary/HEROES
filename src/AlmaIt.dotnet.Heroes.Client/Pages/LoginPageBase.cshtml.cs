namespace AlmaIt.Dotnet.Heroes.Client.Pages
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using Blazor.Extensions.Storage;
    using global::Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.JSInterop;

    public class LoginPageBase : BlazorComponent
    {

        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected SessionStorage SessionStorage { get; set; }

        public CredentialModel User { get; set; } = new CredentialModel();

        public string LoginMessage { get; set; }

        public bool IsLoginSucess { get; set; }

        public AlertType GetAlertType { get => this.IsLoginSucess ? AlertType.success : AlertType.danger; }

        protected async Task SignIn()
        {
            var response = await this.AppState.LoginAsync(this.User);

            this.LoginMessage = response.Message;
            this.IsLoginSucess = response.Success;

            if (this.IsLoginSucess)
            {
                await this.SessionStorage.SetItem<string>("message", this.LoginMessage);
                await this.SessionStorage.SetItem<string>("messageType", this.GetAlertType.ToString());
                this.UriHelper.NavigateTo("/home");
            }
            else
            { this.StateHasChanged(); }
        }
    }
}