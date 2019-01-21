namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    public class AddUserBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        protected UserModel user = new UserModel();

        protected string inputClaim = string.Empty;

        protected bool showAddUserPanel = false;

        protected override void OnInit()
        {
            this.user.Claims = new List<string>();
        }

        protected void ToggleShowAddUser()
        {
            this.showAddUserPanel = !this.showAddUserPanel;
            StateHasChanged();
        }

        protected async Task CreateUser()
        {
            await Http.SendJsonAsync(HttpMethod.Post, "/api/user", user);
        }

        protected void AddClaim()
        {
            this.user.Claims.Add($"{this.inputClaim}:True");
            StateHasChanged();
        }

        protected void RemoveClaim(string claim)
        {
            this.user.Claims.RemoveAll(x => x.Contains(claim));
        }
    }
}