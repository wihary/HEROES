namespace AlmaIt.dotnet.Heroes.Client.ViewModel
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Blazor.Extensions.Storage;
    using System;
    using AlmaIt.dotnet.Heroes.Shared.Business;
    using Dotnet.JsonIdentityProvider.IdentityProvider.Model;
    using Microsoft.AspNetCore.Blazor;
    using Newtonsoft.Json;
    using System.Text;
    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using Microsoft.AspNetCore.Blazor.Services;

    /// <summary>
    ///     This class is use as an authentification manager for the client
    ///     it contains the loggedIn status and all the methods need to login and logout with the server
    /// </summary>
    public class AppState
    {
        private readonly HttpClient httpClient;
        private readonly SessionStorage sessionStorage;
        private readonly IUriHelper uriHelper;

        public bool IsLoggedin { get; private set; }

        public event EventHandler UserHasLoggedIn;
        public event EventHandler UserHasLoggedOut;

        public AppState(HttpClient httpClient, SessionStorage localStorage, IUriHelper uriHelper)
        {
            this.httpClient = httpClient;
            this.sessionStorage = localStorage;
            this.uriHelper = uriHelper;
        }

        /// <summary>
        ///     Simple method that checks if there is a JWT avaible and if its valid
        /// </summary>
        /// <value></value>
        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                var tokenInfo = await this.sessionStorage.GetItem<TokenInfo>("authToken");
                if (tokenInfo == null || string.IsNullOrEmpty(tokenInfo.Token) || tokenInfo.Expired.CompareTo(DateTime.Now) <= 0)
                {
                    this.IsLoggedin = false;
                }
                else
                {
                    this.IsLoggedin = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return this.IsLoggedin;
        }

        public async Task<(bool Success, string Message)> LoginAsync(CredentialModel user)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var result = await this.httpClient.PostAsync("/api/auth/token", jsonContent);

                if (!result.IsSuccessStatusCode)
                { return (false, result.ReasonPhrase); }

                // Get token from API response, add username to it backing info into session storage
                var token = JsonConvert.DeserializeObject<TokenInfo>(await result.Content.ReadAsStringAsync());
                token.UserName = user.UserName;
                await this.sessionStorage.SetItem<TokenInfo>("authToken", token);

                // Ensure that everything is set correctly
                if (await this.IsLoggedInAsync())
                {
                    this.OnUserLoggedIn(EventArgs.Empty);
                    return (true, $"Successfully logged in, Welcome {token.UserName} !");
                }
                else
                {
                    return (false, $"Session storage error occured, token could not be saved !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task Logout()
        {
            await this.sessionStorage.RemoveItem("authToken");

            if (!await this.IsLoggedInAsync())
            {
                await this.sessionStorage.SetItem<string>("message", "Your are now disconnected !");
                await this.sessionStorage.SetItem<string>("messageType", AlertType.success.ToString());

                this.OnUserLoggedOut(EventArgs.Empty);
            }
            else
            {
                await this.sessionStorage.SetItem<string>("message", "Logout failed for unknow reason :'(");
                await this.sessionStorage.SetItem<string>("messageType", AlertType.warning.ToString());

                this.OnUserLoggedOut(EventArgs.Empty);
            }

            this.uriHelper.NavigateTo("/");
        }

        private async Task SetAuthorizationHeader()
        {
            if (!this.httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await this.sessionStorage.GetItem<string>("authToken");
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        protected virtual void OnUserLoggedIn(EventArgs e)
        {
            if (this.UserHasLoggedIn != null)
                this.UserHasLoggedIn(this, e);
        }

        protected virtual void OnUserLoggedOut(EventArgs e)
        {
            if (this.UserHasLoggedOut != null)
                this.UserHasLoggedOut(this, e);
        }
    }
}