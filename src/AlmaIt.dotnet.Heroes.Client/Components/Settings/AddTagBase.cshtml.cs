namespace AlmaIt.dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using Newtonsoft.Json;

    public class AddTagBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Parameter]
        Func<bool, Task> TagCreated { get; set; }

        public ObjectTag tag { get; set; } = new ObjectTag();

        protected bool IsVisible = false;

        protected List<Color> AvailableColor = new List<Color>();

        /// <summary>
        ///     This method is the entry point to the blazor component rendering
        /// </summary>
        /// <returns></returns>
        protected override void OnInit()
        {    
            foreach (string knownColor in Enum.GetNames(typeof(KnownColor)))
            {
                this.AvailableColor.Add(Color.FromName(knownColor));
            }
        }

        /// <summary>
        ///     Method that send a post request to server with Tag object in order to create it
        /// </summary>
        /// <returns></returns>
        protected async Task CreateTag()
        {
            await Http.SendJsonAsync(HttpMethod.Post, "/api/tag", this.tag);
            await this.TagCreated(true);
        }

        protected void ResetPanelState()
        {
            this.tag = new ObjectTag();
            this.StateHasChanged();
        }

        protected void ToggleVisibility()
        {
            this.IsVisible = !this.IsVisible;
            this.StateHasChanged();
        }
    }
}