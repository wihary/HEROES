#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Components
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>
    /// Base class of AddSerie component.
    /// </summary>
    public class AddSerieBase : BlazorComponent
    {
        /// <summary>
        /// Comic serie to add.
        /// </summary>
        protected readonly ComicSeries comicSerie = new ComicSeries();

        /// <summary>
        /// Boolean to determine whether show add panel.
        /// </summary>
        protected bool showAddSeriePanel;

        /// <summary>
        /// Gets or sets the event raises when a comic serie added.
        /// </summary>
        [Parameter]
        protected Func<bool, Task> ComicSerieAdded { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>
        /// Method to create a comic serie.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task CreateComicSerie()
        {
            await this.Http.SendJsonAsync(HttpMethod.Post, "/api/ComicSerie", this.comicSerie);

            await this.ComicSerieAdded(true);
        }

        /// <summary>
        /// Method to toggle add serie panel.
        /// </summary>
        protected void ToggleShowAddSerie()
        {
            this.showAddSeriePanel = !this.showAddSeriePanel;
            this.StateHasChanged();
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
