namespace AlmaIt.Dotnet.Heroes.Client.ViewModel
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Business;

    using Blazor.Extensions.Storage;

    using global::Dotnet.JsonIdentityProvider.IdentityProvider.Model;

    using Microsoft.AspNetCore.Blazor.Services;

    using Newtonsoft.Json;

    /// <summary>
    /// This class is use as an authentification manager for the client it contains the loggedIn status and all the methods need to login and logout with the server.
    /// </summary>
    public sealed class AppState
    {
        private readonly HttpClient httpClient;

        private readonly SessionStorage sessionStorage;

        private readonly IUriHelper uriHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppState"/> class.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        /// <param name="localStorage">Session storage.</param>
        /// <param name="uriHelper">Uri helper.</param>
        public AppState(HttpClient httpClient, SessionStorage localStorage, IUriHelper uriHelper)
        {
            this.httpClient = httpClient;
            this.sessionStorage = localStorage;
            this.uriHelper = uriHelper;
        }

        /// <summary>
        /// Event represent when a user log in.
        /// </summary>
        public event EventHandler UserHasLoggedIn;

        /// <summary>
        /// Event represent when a user log out.
        /// </summary>
        public event EventHandler UserHasLoggedOut;

        /// <summary>
        /// Gets a value indicating whether the user is log.
        /// </summary>
        public bool IsLoggedIn { get; private set; }

        /// <summary>
        /// Method to get the connected user name.
        /// </summary>
        /// <returns>Return the username of connected user.</returns>
        public async Task<string> GetConnectedUserName()
        {
            Console.WriteLine($"this.IsLoggedIn : {this.IsLoggedIn}");
            return !this.IsLoggedIn ? string.Empty : (await this.sessionStorage.GetItem<TokenInfo>("authToken"))?.UserName;
        }

        /// <summary>Simple method that checks if there is a JWT avaible and if its valid.</summary>
        /// <returns>Return a value indicating whether the user is log in.</returns>
        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                var tokenInfo = await this.sessionStorage.GetItem<TokenInfo>("authToken");
                if (tokenInfo == null || string.IsNullOrEmpty(tokenInfo.Token) || tokenInfo.Expired.CompareTo(DateTime.Now) <= 0)
                {
                    this.CleanAuthorizationHeader();
                    this.IsLoggedIn = false;
                }
                else
                {
                    await this.SetAuthorizationHeader().ConfigureAwait(false);
                    this.IsLoggedIn = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return this.IsLoggedIn;
        }

        /// <summary>
        /// Method to log a user.
        /// </summary>
        /// <param name="user">User to log.</param>
        /// <returns>Return a <see cref="Tuple"/> that indicating whether the user is authenticate and a message.</returns>
        public async Task<(bool Success, string Message)> LoginAsync(CredentialModel user)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var result = await this.httpClient.PostAsync("/api/auth/token", jsonContent);

                if (!result.IsSuccessStatusCode)
                {
                    return (false, result.ReasonPhrase);
                }

                // Get token from API response, add username to it backing info into session storage
                var token = JsonConvert.DeserializeObject<TokenInfo>(await result.Content.ReadAsStringAsync());
                token.UserName = user.UserName;
                await this.sessionStorage.SetItem("authToken", token);

                // Ensure that everything is set correctly, including headers with bearer authentification
                if (!await this.IsLoggedInAsync().ConfigureAwait(false))
                {
                    return (false, "Session storage error occured, token could not be saved !");
                }

                this.OnUserLoggedIn(EventArgs.Empty);
                return (true, $"Successfully logged in, Welcome {token.UserName} !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Method to log out a user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Logout()
        {
            await this.sessionStorage.RemoveItem("authToken");

            if (!await this.IsLoggedInAsync().ConfigureAwait(false))
            {
                await this.sessionStorage.SetItem("message", "Your are now disconnected !");
                await this.sessionStorage.SetItem("messageType", AlertType.Success.ToString());

                this.OnUserLoggedOut(EventArgs.Empty);
            }
            else
            {
                await this.sessionStorage.SetItem("message", "Logout failed for unknow reason :'(");
                await this.sessionStorage.SetItem("messageType", AlertType.Warning.ToString());

                this.OnUserLoggedOut(EventArgs.Empty);
            }

            this.uriHelper.NavigateTo("/");
        }

        private void OnUserLoggedIn(EventArgs e) => this.UserHasLoggedIn?.Invoke(this, e);

        private void OnUserLoggedOut(EventArgs e) => this.UserHasLoggedOut?.Invoke(this, e);

        private void CleanAuthorizationHeader()
        {
            if (this.httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                this.httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        private async Task SetAuthorizationHeader()
        {
            if (!this.httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var tokenInfo = await this.sessionStorage.GetItem<TokenInfo>("authToken");
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.Token);
            }
        }
    }
}
