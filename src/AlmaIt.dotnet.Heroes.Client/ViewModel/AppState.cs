namespace AlmaIt.dotnet.Heroes.Client.ViewModel
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using Blazor.Extensions.Storage;
    using System;
    using AlmaIt.dotnet.Heroes.Shared.Business;

    /// <summary>
    ///     This class is use as an authentification manager for the client
    ///     it contains the loggedIn status and all the methods need to login and logout with the server
    /// </summary>
    public class AppState
    {
        private readonly HttpClient httpClient;
        private readonly SessionStorage localStorage;

        public bool IsLoggedin { get; set; }

        public AppState(HttpClient httpClient, SessionStorage localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        /// <summary>
        ///     Simple method that checks if there is a JWT avaible and if its valid
        /// </summary>
        /// <value></value>
        public async Task<bool> IsLoggedInAsync()
        {
            Console.WriteLine("called");
            try
            {
                var tokenInfo = await this.localStorage.GetItem<TokenInfo>("authToken");
                if (tokenInfo == null || string.IsNullOrEmpty(tokenInfo.Token) || tokenInfo.ExpirationDate.CompareTo(DateTime.Now) <= 0)
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

        public void Login()
        {

        }

        public void Logout()
        {

        }

        private async Task SetAuthorizationHeader()
        {
            if (!this.httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await this.localStorage.GetItem<string>("authToken");
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}