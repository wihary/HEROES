namespace AlmaIt.Dotnet.Heroes.Client.ViewModel.Components
{
    using System;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using Blazor.Extensions.Storage;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>
    ///     Code-behind of the TopAlertMessage component
    /// </summary>
    public class TopAlertMessageBase : BlazorComponent
    {
        /// <summary>
        ///     Inject SessionStorage to enable client side data saves in browser tab session storage
        /// </summary>
        [Inject]
        protected SessionStorage SessionStorage { get; set; }

        /// <summary>
        ///     Message à communiquer sous forme d'alerte bootstrap a l'utilisateur
        /// </summary>
        /// <value></value>
        [Parameter]
        public string Message { get; set; }

        /// <summary>
        ///     Type d'alerte bootstrap a afficher
        /// </summary>
        /// <value></value>
        [Parameter]
        public AlertType AlertType { get; set; }

        /// <summary>
        ///     Method called when blazor component is first initialized
        /// </summary>
        protected override async Task OnInitAsync()
        {
            await this.DisplayAvailableMessageAsync();
        }

        /// <summary>
        ///     Display available message from browser Session Storage
        /// </summary>
        private async Task DisplayAvailableMessageAsync()
        {
            var message = await this.SessionStorage.GetItem<string>("message");

            if (!string.IsNullOrEmpty(message))
            {
                this.AlertType = (AlertType)Enum.Parse(typeof(AlertType), await this.SessionStorage.GetItem<string>("messageType"));
                this.Message = message;

                await this.SessionStorage.RemoveItem("message");
                await this.SessionStorage.RemoveItem("messageType");
            }
        }
    }
}