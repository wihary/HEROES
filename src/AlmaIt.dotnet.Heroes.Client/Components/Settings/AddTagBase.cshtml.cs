namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    public class AddTagBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Parameter]
        Func<bool, Task> TagCreated { get; set; }

        public ObjectTag Tag { get; set; } = new ObjectTag();

        protected bool IsVisible = false;

        protected Color[] AvailableColor = { Color.AliceBlue, Color.Blue, Color.Green, Color.Red, Color.Yellow };


        /// <summary>
        ///     Method that send a post request to server with Tag object in order to create it
        /// </summary>
        /// <returns></returns>
        protected async Task CreateTag()
        {
            await Http.SendJsonAsync(HttpMethod.Post, "/api/tag", this.Tag);

            await this.TagCreated(true);
        }

        protected void ResetPanelState()
        {
            this.Tag = new ObjectTag();
            this.StateHasChanged();
        }

        protected void ToggleVisibility()
        {
            this.IsVisible = !this.IsVisible;
            this.StateHasChanged();
        }
    }
}