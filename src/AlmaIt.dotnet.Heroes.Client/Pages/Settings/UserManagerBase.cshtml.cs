namespace AlmaIt.Dotnet.Heroes.Client.Pages.Settings
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.Components.Settings;
    using Microsoft.AspNetCore.Blazor.Components;

    public class UserManagerBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        public UserList UserListComponent { get; set; }

        protected async Task OnUserAdded(bool success)
        {
            if (success)
            { await this.UserListComponent.RefreshUserListAsync(); }

            this.StateHasChanged();
        }
    }
}