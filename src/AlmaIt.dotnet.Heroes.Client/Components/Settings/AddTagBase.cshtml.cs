#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Components.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Class of add tag component.</summary>
    public class AddTagBase : BlazorComponent
    {
        /// <summary>List of available color.</summary>
        private protected readonly List<Color> availableColor = new List<Color>();

        /// <summary>Tag to add.</summary>
        private protected readonly ObjectTag tag = new ObjectTag();

        /// <summary>Value indicating whether the component is visible.</summary>
        private protected bool isVisible;

        [Inject]
        private HttpClient Http { get; set; }

        [Parameter]
        private Func<bool, Task> TagCreated { get; set; }

        /// <summary>Method that send a post request to server with Tag object in order to create it.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task CreateTag()
        {
            await this.Http.SendJsonAsync(HttpMethod.Post, "/api/tag", this.tag);
            await this.TagCreated(true).ConfigureAwait(false);
            this.StateHasChanged();
        }

        /// <summary>Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree.</summary>
        protected override void OnInit()
        {
            foreach (var knownColor in Enum.GetNames(typeof(KnownColor)))
            {
                this.availableColor.Add(Color.FromName(knownColor));
            }
        }

        /// <summary>Method to toggle component visibility.</summary>
        protected void ToggleVisibility()
        {
            this.isVisible = !this.isVisible;
            this.StateHasChanged();
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
