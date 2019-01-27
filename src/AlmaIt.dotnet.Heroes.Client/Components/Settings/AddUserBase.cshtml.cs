namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using Newtonsoft.Json;

    public class AddUserBase : BlazorComponent
    {
        [Inject]
        private HttpClient Http { get; set; }

        [Parameter]
        public Func<bool, Task> UserAdded { get; set; }

        protected UserModel user = new UserModel();

        protected string inputClaim = string.Empty;

        protected bool ShowAddUserPanel;

        protected string Message { get; set; }

        protected AlertType MessageType { get; set; }

        protected override void OnInit()
        {
            this.user.Claims = new List<string>();
        }

        protected void ToggleShowAddUser()
        {
            this.ShowAddUserPanel = !this.ShowAddUserPanel;
            StateHasChanged();
        }

        protected async Task CreateUser()
        {
            var response = await this.Http.PostAsync("/api/user", new StringContent(JsonConvert.SerializeObject(this.user),
                Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                this.Message = $"User '{this.user.Name}' has been added successfully !";
                this.MessageType = AlertType.success;
                this.ResetUserForm();
                await this.UserAdded(true).ConfigureAwait(false);
            }
            else
            {
                this.Message = $"Error while creating user '{this.user.Name}' = [{(int)response.StatusCode}]{response.ReasonPhrase}";
                this.MessageType = AlertType.danger;
            }

            this.StateHasChanged();
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

        protected void ResetUserForm()
        {
            this.user = new UserModel();
            this.user.Claims = new List<string>();
            this.inputClaim = string.Empty;
            this.ShowAddUserPanel = false;
        }
    }
}