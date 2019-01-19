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

    public class EditComicBase : BlazorComponent
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        public Func<bool, Task> EditCompleted { get; set; }

        [Parameter]
        public ComicBook EditedComicBook { get; set; }

        protected List<ComicSeries> comicSerieList = new List<ComicSeries>();
        protected int selectedComicSerie;
        protected ComicBookStatus selectedBookStatus = ComicBookStatus.None;

        /// <summary>
        /// Referential list of all available tags in Db
        /// </summary>
        /// <typeparam name="ObjectTag"></typeparam>
        /// <returns></returns>
        protected List<ObjectTag> objectTagList = new List<ObjectTag>();

        protected string SelectedTag = string.Empty;


        protected override async Task OnInitAsync()
        {
            this.objectTagList = await Http.GetJsonAsync<List<ObjectTag>>("api/tag");
            this.comicSerieList = await Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");

            if (this.EditedComicBook.ComicSerieId.HasValue)
            { this.selectedComicSerie = this.EditedComicBook.ComicSerieId.Value; }

            this.selectedBookStatus = this.EditedComicBook.Status;
        }

        protected async Task EditionCompleted(bool success)
        {
            await this.EditCompleted(success).ConfigureAwait(false);
        }

        protected async Task UpdateComicBook()
        {
            this.EditedComicBook.ComicSerieId = this.selectedComicSerie;
            this.EditedComicBook.Status = this.selectedBookStatus;

            await Http.SendJsonAsync(HttpMethod.Put, "/api/ComicBook", this.EditedComicBook);
            await this.EditionCompleted(true).ConfigureAwait(false);
        }
        protected void AddTagSelected()
        {
            var selectedTag = this.objectTagList.FirstOrDefault(tag => tag.Name == this.SelectedTag);

            try
            {
                if (this.EditedComicBook.RelatedTags == null)
                { this.EditedComicBook.RelatedTags = new List<ComicBookTags>(); }

                this.EditedComicBook.RelatedTags.Add(
                    new ComicBookTags
                    {
                        Tag = selectedTag
                    });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            StateHasChanged();
        }

        protected void RemoveTag(string tagName)
        {
            var removedTag = this.EditedComicBook.RelatedTags.ToList().FirstOrDefault(x => x.Tag.Name == tagName);
            this.EditedComicBook.RelatedTags.Remove(removedTag);
        }
    }
}