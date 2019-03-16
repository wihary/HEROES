using System.Net.Http;
using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace AlmaIt.Dotnet.Heroes.Client.ViewModel
{
    /// <summary>
    ///     Base class for blazor component that implements common elements
    /// </summary>
    public class ViewModelBase : BlazorComponent
    {
        /// <summary>
        ///     Inject common application context
        /// </summary>
        [Inject]
        public AppState AppState { get; set; }

        /// <summary>
        ///     Inject SessionStorage to enable client side data saves in browser tab session storage
        /// </summary>
        [Inject]
        private SessionStorage SessionStorage { get; set; }

        /// <summary>
        ///     Inject SessionStorage to enable client side data saves in browser local storage
        /// </summary>
        [Inject]
        private LocalStorage LocalStorage { get; set; }

        /// <summary>
        ///     Inject code behind navigation component from Blazor
        /// </summary>
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        /// <summary>
        ///     Inject Blazor Browser HttpClient component so that client get send HTTP rest request to server
        /// </summary>
        [Inject]
        protected HttpClient Http { get; set; }

        /// <summary>
        ///     Message to user display as a bootstrap alert component
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Bootstrap Alert type to display
        /// </summary>
        public AlertType Type { get; set; }
    }
}