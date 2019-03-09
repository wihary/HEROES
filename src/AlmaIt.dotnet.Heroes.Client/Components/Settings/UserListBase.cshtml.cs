#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;

    using global::Dotnet.JsonIdentityProvider.IdentityProvider.Model;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    /// <summary>
    /// Class of user list component.
    /// </summary>
    public class UserListBase : BlazorComponent
    {
        /// <summary>
        /// Gets the alert message.
        /// </summary>
        protected string Message { get; private set; }

        /// <summary>
        /// Gets the alert type.
        /// </summary>
        protected AlertType MessageType { get; private set; }

        /// <summary>
        /// Gets the user list.
        /// </summary>
        protected List<UserModel> Users { get; private set; }

        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>
        /// Method to refresh user list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task RefreshUserListAsync()
        {
            var response = await this.Http.GetAsync("api/user", HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            using (var jsonReader = new JsonTextReader(streamReader))
                            {
                                var serializer = new JsonSerializer();
                                this.Users = serializer.Deserialize<List<UserModel>>(jsonReader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message;
                    this.MessageType = AlertType.Warning;
                }
            }
            else
            {
                this.Message = $"[{(int)response.StatusCode}] : {response.ReasonPhrase}";
                this.MessageType = AlertType.Danger;
            }

            if (this.Users == null)
            {
                this.Users = new List<UserModel>();
            }

            this.StateHasChanged();
        }

        /// <summary>
        /// Method to delete a user.
        /// </summary>
        /// <param name="username">User's name to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task DeleteUser(string username)
        {
            var response = await this.Http.DeleteAsync($"api/user/{username}");

            if (response.IsSuccessStatusCode)
            {
                this.Message = $"User '{username}' has been removed successfully !";
                this.MessageType = AlertType.Success;
                await this.RefreshUserListAsync().ConfigureAwait(false);
            }
            else
            {
                this.Message = $"Error occured while removing User '{username}' = [{(int)response.StatusCode}] : {response.ReasonPhrase}";
                this.MessageType = AlertType.Danger;
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync() => await this.RefreshUserListAsync();
    }
}
#pragma warning restore SA1401 // Fields should be private
