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
    /// Base class of EditSerie component.
    /// </summary>
    public class EditSerieBase : BlazorComponent
    {
        /// <summary>
        /// Gets or sets the comic series to edit.
        /// </summary>
        [Parameter]
        protected ComicSeries SelectedSerie { get; set; }

        /// <summary>
        /// Gets or sets the event raises when a comic serie edited.
        /// </summary>
        [Parameter]
        protected Func<bool, Task> EditCompleted { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>
        /// Method to raise event.
        /// </summary>
        /// <param name="success">The sucess or not.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        protected async Task EditionCompleted(bool success)
            => await this.EditCompleted(success);

        /// <summary>
        /// Method to update serie.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task UpdateComicSerie()
        {
            await this.Http.SendJsonAsync(HttpMethod.Put, "/api/ComicSerie", this.SelectedSerie);
            await this.EditionCompleted(true);
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
