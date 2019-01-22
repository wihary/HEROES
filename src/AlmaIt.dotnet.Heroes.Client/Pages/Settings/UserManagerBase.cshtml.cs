namespace AlmaIt.dotnet.Heroes.Client.Pages.Settings
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Client.Components.Settings;
    using Microsoft.AspNetCore.Blazor.Components;

    public class UserManagerBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        public UserList UserListComponent { get; set; }

        protected async Task OnUserAdded(bool success)
        {
            if(success)
                await this.UserListComponent.RefreshUserListAsync();

            this.StateHasChanged();
        }
    }
}