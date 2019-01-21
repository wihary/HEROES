namespace AlmaIt.dotnet.Heroes.Client.Pages
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Client.ViewModel;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor.Components;

    public class LoginPageBase : BlazorComponent
    {

        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected HttpClient HttpClient { get; set; }

        public CredentialModel User { get; set; } = new CredentialModel();

        protected async Task SignIn()
        {
            await this.AppState.LoginAsync(this.User);
        }
    }
}