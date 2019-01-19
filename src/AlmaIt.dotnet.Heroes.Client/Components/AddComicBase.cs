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

    public class AddComicBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Parameter]
        Func<bool, Task> ComicBookAdded { get; set; }

        /// <summary>
        /// Referential list of all available series in DB
        /// </summary>
        /// <typeparam name="ComicSeries"></typeparam>
        /// <returns></returns>
        protected List<ComicSeries> comicSerieList = new List<ComicSeries>();

        /// <summary>
        /// Referential list of all available tags in Db
        /// </summary>
        /// <typeparam name="ObjectTag"></typeparam>
        /// <returns></returns>
        protected List<ObjectTag> objectTagList = new List<ObjectTag>();

        protected string SelectedTag = string.Empty;
        protected bool ShowAddComicPanel = false;
        protected ComicBook comicBook = new ComicBook();

        protected int selectedComicSerie = 0;
        protected ComicBookStatus selectedBookStatus = ComicBookStatus.None;



        protected override async Task OnInitAsync()
        {
            this.comicSerieList = await Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");
            this.objectTagList = await Http.GetJsonAsync<List<ObjectTag>>("api/tag");
        }

        protected async Task CreateComicBook()
        {
            if (selectedComicSerie != 0)
                comicBook.ComicSerieId = selectedComicSerie;
            comicBook.Status = selectedBookStatus;

            await Http.SendJsonAsync(HttpMethod.Post, "/api/ComicBook", comicBook);

            await this.ComicBookAdded(true);
            StateHasChanged();
        }

        protected void ToggleShowAddComic()
        {
            this.ShowAddComicPanel = !this.ShowAddComicPanel;
            StateHasChanged();
        }

        protected void AddTagSelected()
        {
            var selectedTag = this.objectTagList.FirstOrDefault(tag => tag.Name == this.SelectedTag);

            try
            {
                if(this.comicBook.RelatedTags == null)
                    this.comicBook.RelatedTags = new List<ComicBookTags>();

                this.comicBook.RelatedTags.Add(
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
            var removedTag = this.comicBook.RelatedTags.ToList().FirstOrDefault(x => x.Tag.Name == tagName);
            this.comicBook.RelatedTags.Remove(removedTag);
        }
    }
}