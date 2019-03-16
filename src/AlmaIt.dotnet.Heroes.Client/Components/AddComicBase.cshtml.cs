#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Add comic composant base.</summary>
    public class AddComicBase : BlazorComponent
    {
        /// <summary>Th comic book to add.</summary>
        private protected readonly ComicBook comicBook = new ComicBook();

        /// <summary>Referential list of all available series in DB.</summary>
        private protected List<ComicSeries> comicSerieList = new List<ComicSeries>();

        /// <summary>Referential list of all available tags in Db.</summary>
        private protected List<ObjectTag> objectTagList = new List<ObjectTag>();

        /// <summary>Status of selected book.</summary>
        private protected ComicBookStatus selectedBookStatus = ComicBookStatus.None;

        /// <summary>Selected comic serie id.</summary>
        private protected int selectedComicSerie;

        /// <summary>Selected tag.</summary>
        private protected string selectedTagName = string.Empty;

        /// <summary>Gets or sets a value indicating whether the add comic panel is visible or not.</summary>
        private protected bool showAddComicPanel;

        /// <summary>Gets or sets the event raises when a comic book added.</summary>
        [Parameter]
        private protected Func<bool, Task> ComicBookAdded { get; set; }

        /// <summary>Gets or sets the http client.</summary>
        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>
        ///     Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree. Override this method if you will perform
        ///     an asynchronous operation and want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            this.comicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");
            this.objectTagList = await this.Http.GetJsonAsync<List<ObjectTag>>("api/tag");
        }

        /// <summary>Method to add selected tag.</summary>
        private protected void AddTagSelected()
        {
            var selectedTag = this.objectTagList.FirstOrDefault(tag => tag.Name == this.selectedTagName);

            try
            {
                if (this.comicBook.RelatedTags == null)
                {
                    this.comicBook.RelatedTags = new List<ComicBookTags>();
                }

                this.comicBook.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = selectedTag,
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            this.StateHasChanged();
        }

        /// <summary>Async method to create a comic book.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        private protected async Task CreateComicBook()
        {
            if (this.selectedComicSerie != 0)
            {
                this.comicBook.ComicSerieId = this.selectedComicSerie;
            }

            this.comicBook.Status = this.selectedBookStatus;

            await this.Http.SendJsonAsync(HttpMethod.Post, "/api/ComicBook", this.comicBook);

            await this.ComicBookAdded(true).ConfigureAwait(false);
            this.StateHasChanged();
        }

        /// <summary>
        /// Method to remove a tag.
        /// </summary>
        /// <param name="tagName">Name of tag to remove.</param>
        private protected void RemoveTag(string tagName)
        {
            var removedTag = this.comicBook.RelatedTags.ToList().FirstOrDefault(x => x.Tag.Name == tagName);
            this.comicBook.RelatedTags.Remove(removedTag);
        }

        /// <summary>
        /// Method to togqle the visibility of add comic panel.
        /// </summary>
        private protected void ToggleShowAddComic()
        {
            this.showAddComicPanel = !this.showAddComicPanel;
            this.StateHasChanged();
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
