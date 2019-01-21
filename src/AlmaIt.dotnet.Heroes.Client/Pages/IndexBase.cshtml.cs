namespace AlmaIt.dotnet.Heroes.Client.Pages
{
    using System;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using Blazor.Extensions.Storage;
    using Microsoft.AspNetCore.Blazor.Components;

    public class IndexBase : BlazorComponent
    {
        [Inject]
        private SessionStorage SessionStorage { get; set; }

        protected string Message { get; set; }

        protected AlertType Type { get; set; }

        protected override async Task OnInitAsync()
        {
            var message= await this.SessionStorage.GetItem<string>("message");

            if (!string.IsNullOrEmpty(message))
            {
                var messageType = (AlertType)Enum.Parse(typeof(AlertType), await this.SessionStorage.GetItem<string>("messageType"));
                await this.SessionStorage.RemoveItem("message");
                await this.SessionStorage.RemoveItem("messageType");
                this.Message = message;
                this.Type = messageType;
            }
        }
    }
}