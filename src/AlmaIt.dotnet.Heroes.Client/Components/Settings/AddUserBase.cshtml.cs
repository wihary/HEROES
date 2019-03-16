#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;

    using global::Dotnet.JsonIdentityProvider.IdentityProvider.Model;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    /// <summary>Class of add user component.</summary>
    public class AddUserBase : BlazorComponent
    {
        /// <summary>User claim.</summary>
        private protected string inputClaim = string.Empty;

        /// <summary>A value indicating whether the add user paner is visible.</summary>
        private protected bool showAddUserPanel;

        /// <summary>User to add.</summary>
        private protected UserModel user = new UserModel();

        /// <summary>Gets the alert message.</summary>
        private protected string Message { get; private set; }

        /// <summary>Gets the message type.</summary>
        private protected AlertType MessageType { get; private set; }

        /// <summary>Gets or sets the http client.</summary>
        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>Gets or sets function raises when a user added.</summary>
        [Parameter]
        private Func<bool, Task> UserAdded { get; set; }

        /// <summary>Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree.</summary>
        protected override void OnInit() => this.user.Claims = new List<string>();

        /// <summary>Method to add claim.</summary>
        private protected void AddClaim()
        {
            this.user.Claims.Add($"{this.inputClaim}:True");
            this.StateHasChanged();
        }

        /// <summary>Method to create a user.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        private protected async Task CreateUser()
        {
            var response = await this.Http.PostAsync("/api/user", new StringContent(JsonConvert.SerializeObject(this.user), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                this.Message = $"User '{this.user.Name}' has been added successfully !";
                this.MessageType = AlertType.Success;
                this.ResetUserForm();
                await this.UserAdded(true).ConfigureAwait(false);
            }
            else
            {
                this.Message = $"Error while creating user '{this.user.Name}' = [{(int)response.StatusCode}]{response.ReasonPhrase}";
                this.MessageType = AlertType.Danger;
            }

            this.StateHasChanged();
        }

        /// <summary>Method to remove claim.</summary>
        /// <param name="claim">Claim to remove.</param>
        private protected void RemoveClaim(string claim) => this.user.Claims.RemoveAll(x => x.Contains(claim));

        /// <summary>Method to toggle add user component.</summary>
        private protected void ToggleShowAddUser()
        {
            this.showAddUserPanel = !this.showAddUserPanel;
            this.StateHasChanged();
        }

        private void ResetUserForm()
        {
            this.user = new UserModel { Claims = new List<string>() };
            this.inputClaim = string.Empty;
            this.showAddUserPanel = false;
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
