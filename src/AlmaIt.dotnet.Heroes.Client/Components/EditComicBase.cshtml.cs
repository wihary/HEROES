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

    /// <summary>
    /// Class of edit comic component.
    /// </summary>
    public class EditComicBase : BlazorComponent
    {
        /// <summary>
        /// List of comic series.
        /// </summary>
        private protected List<ComicSeries> comicSerieList = new List<ComicSeries>();

        /// <summary>Referential list of all available tags in Db.</summary>
        private protected List<ObjectTag> objectTagList = new List<ObjectTag>();

        /// <summary>
        /// Status of selected book.
        /// </summary>
        private protected ComicBookStatus selectedBookStatus = ComicBookStatus.None;

        /// <summary>
        /// Selected id comic serie.
        /// </summary>
        private protected int selectedComicSerie;

        /// <summary>
        /// Selected tag name.
        /// </summary>
        private protected string selectedTagName = string.Empty;

        /// <summary>
        /// Gets or sets comic book to edit.
        /// </summary>
        [Parameter]
        protected ComicBook EditedComicBook { get; set; }

        [Parameter]
        private Func<bool, Task> EditCompleted { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            this.objectTagList = await this.Http.GetJsonAsync<List<ObjectTag>>("api/tag");
            this.comicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");

            if (this.EditedComicBook.ComicSerieId.HasValue)
            {
                this.selectedComicSerie = this.EditedComicBook.ComicSerieId.Value;
            }

            this.selectedBookStatus = this.EditedComicBook.Status;
        }

        /// <summary>
        /// Method to add selected tag.
        /// </summary>
        private protected void AddTagSelected()
        {
            var selectedTag = this.objectTagList.FirstOrDefault(tag => tag.Name == this.selectedTagName);

            try
            {
                if (this.EditedComicBook.RelatedTags == null)
                {
                    this.EditedComicBook.RelatedTags = new List<ComicBookTags>();
                }

                this.EditedComicBook.RelatedTags.Add(
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

        /// <summary>
        /// Method call when the edition of comic is completed.
        /// </summary>
        /// <param name="success">A value indicating whether the ediotion is a success.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        private protected async Task EditionCompleted(bool success) => await this.EditCompleted(success).ConfigureAwait(false);

        /// <summary>
        /// Method to remove tag.
        /// </summary>
        /// <param name="tagName">Nome of tag to remove.</param>
        private protected void RemoveTag(string tagName)
        {
            var removedTag = this.EditedComicBook.RelatedTags.ToList().FirstOrDefault(x => x.Tag.Name == tagName);
            this.EditedComicBook.RelatedTags.Remove(removedTag);
        }

        /// <summary>
        /// Methode to update comic.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private protected async Task UpdateComicBook()
        {
            this.EditedComicBook.ComicSerieId = this.selectedComicSerie;
            this.EditedComicBook.Status = this.selectedBookStatus;

            await this.Http.SendJsonAsync(HttpMethod.Put, "/api/ComicBook", this.EditedComicBook);
            await this.EditionCompleted(true).ConfigureAwait(false);
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
