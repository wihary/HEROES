namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Newtonsoft.Json;

    public class UserListBase : BlazorComponent
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        protected List<UserModel> Users { get; set; }

        protected bool IsErrorMessage { get; set; } = false;

        protected string Message { get; set; }

        protected AlertType MessageType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            await this.RefreshUserListAsync();
        }

        public async Task RefreshUserListAsync()
        {
            var response = await Http.GetAsync($"api/user", HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        this.Users = serializer.Deserialize<List<UserModel>>(jsonReader);
                    }
                }
                catch (Exception ex)
                {
                    this.IsErrorMessage = true;
                    this.Message = ex.Message;
                    this.MessageType = AlertType.warning;
                }
            }
            else
            {
                this.Message = $"[{(int)response.StatusCode}] : {response.ReasonPhrase}";
                this.MessageType = AlertType.danger;
            }

            if (this.Users == null)
                this.Users = new List<UserModel>();

            this.StateHasChanged();
        }

        protected async Task DeleteUser(string username)
        {
            var response = await Http.DeleteAsync($"api/user/{username}");

            if (response.IsSuccessStatusCode)
            {
                this.Message = $"User '{username}' has been removed successfully !";
                this.MessageType = AlertType.success;
                await this.RefreshUserListAsync();
            }
            else
            {
                this.Message = $"Error occured while removing User '{username}' = [{(int)response.StatusCode}] : {response.ReasonPhrase}";
                this.MessageType = AlertType.danger;
            }
        }
    }
}