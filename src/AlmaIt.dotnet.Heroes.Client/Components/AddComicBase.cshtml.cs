namespace AlmaIt.dotnet.Heroes.Client.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Shared.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>
    /// Add comic composant base.
    /// </summary>
    public class AddComicBase : BlazorComponent
    {
        /// <summary>
        /// Gest or sets the http client.
        /// </summary>
        [Inject]
        protected HttpClient Http { get; set; }

        /// <summary>
        /// Gets or sets the event raises when a comic book added.
        /// </summary>
        [Parameter]
        protected Func<bool, Task> ComicBookAdded { get; set; }

        /// <summary>
        /// Referential list of all available series in DB.
        /// </summary>
        protected List<ComicSeries> comicSerieList = new List<ComicSeries>();

        /// <summary>
        /// Referential list of all available tags in Db.
        /// </summary>
        /// <typeparam name="ObjectTag"></typeparam>
        /// <returns></returns>
        protected List<ObjectTag> objectTagList = new List<ObjectTag>();

        protected string SelectedTag = string.Empty;
        protected bool ShowAddComicPanel;
        protected ComicBook comicBook = new ComicBook();

        protected int selectedComicSerie;
        protected ComicBookStatus selectedBookStatus = ComicBookStatus.None;



        protected override async Task OnInitAsync()
        {
            this.comicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");
            this.objectTagList = await this.Http.GetJsonAsync<List<ObjectTag>>("api/tag");
        }

        protected async Task CreateComicBook()
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

        protected void ToggleShowAddComic()
        {
            this.ShowAddComicPanel = !this.ShowAddComicPanel;
            this.StateHasChanged();
        }

        protected void AddTagSelected()
        {
            var selectedTag = this.objectTagList.FirstOrDefault(tag => tag.Name == this.SelectedTag);

            try
            {
                if (this.comicBook.RelatedTags == null)
                { this.comicBook.RelatedTags = new List<ComicBookTags>(); }

                this.comicBook.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = selectedTag
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            this.StateHasChanged();
        }

        protected void RemoveTag(string tagName)
        {
            var removedTag = this.comicBook.RelatedTags.ToList().FirstOrDefault(x => x.Tag.Name == tagName);
            this.comicBook.RelatedTags.Remove(removedTag);
        }
    }
}