namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Newtonsoft.Json;

    public class UserListBase : BlazorComponent
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        public List<UserModel> Users { get; set; }

        public bool IsErrorMessage { get; set; } = false;

        public string Message { get; set; }

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
            Console.WriteLine("Refresh request received !");
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
                }
            }
            else
            {
                this.Message = $"[{(int)response.StatusCode}] : {response.ReasonPhrase}";
            }

            if (this.Users == null)
                this.Users = new List<UserModel>();

                this.StateHasChanged();
        }
    }
}