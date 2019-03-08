namespace AlmaIt.Dotnet.Heroes.Client.Pages.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Class representing the comics blazor component of the comics.</summary>
    public class SeriesBrowserBase : BlazorComponent
    {
        private const int PagerSize = 5;

        private const int PageSize = 10;

        private bool asc = true;

        private IEnumerable<ComicSeries> filteredComicSerie = new List<ComicSeries>();

        /// <summary>Gets or sets this current page.</summary>
        protected int CurrentPage { get; set; } = 1;

        /// <summary>Gets or sets the filter.</summary>
        protected string Filter { get; set; } = string.Empty;

        /// <summary>Gets or sets the full comic serie list.</summary>
        protected List<ComicSeries> FullComicSerieList { get; set; }

        /// <summary>Gets or sets the bool indicating whether a serie edition enabled.</summary>
        protected KeyValuePair<int, bool> IsSerieEditionEnabled { get; set; } = new KeyValuePair<int, bool>(0, false);

        /// <summary>Gets or sets the page max.</summary>
        protected int PagerMax { get; set; } = 1;

        /// <summary>Gets or sets the page min.</summary>
        protected int PagerMin { get; set; } = 1;

        /// <summary>Gets or sets the total pages.</summary>
        protected int TotalPages { get; set; } = 1;

        /// <summary>Gets or sets the viewable comic serie.</summary>
        protected IEnumerable<ComicSeries> ViewableComicSerie { get; set; } = new List<ComicSeries>();

        /// <summary>Gets or sets the http client.</summary>
        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>Gets or sets the sended message.</summary>
        [Parameter]
        private Action<string> SendMessage { get; set; }

        /// <summary>Method of reloading data.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public async Task ReloadDataList()
        {
            await this.OnInitAsync();
            this.StateHasChanged();
        }

        /// <summary>Filter application method.</summary>
        protected void ApplySearchFilter()
        {
            this.filteredComicSerie =
                string.IsNullOrEmpty(this.Filter)
                    ? this.FullComicSerieList
                    : this.FullComicSerieList.Where(serie => serie.Name.Contains(this.Filter)).ToList();

            this.StateHasChanged();
            this.ShowPage(this.CurrentPage);
        }

        /// <summary>Method of removing a comic serie.</summary>
        /// <param name="id">Comic serie's id.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task DeleteComicSerie(int id)
        {
            var result = await this.Http.DeleteAsync($"/api/ComicSerie/{id}");
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                this.SendMessage(await result.Content.ReadAsStringAsync());
            }

            await this.OnInitAsync();
            this.StateHasChanged();
        }

        /// <summary>Editing method of a comic serie.</summary>
        /// <param name="id">Comic serie's id.</param>
        protected void EditComicSerie(int id)
        {
            this.IsSerieEditionEnabled = new KeyValuePair<int, bool>(id, true);

            this.StateHasChanged();
        }

        /// <summary>Handler of collection changed.</summary>
        /// <param name="success">Value indicating whether the collection has changed.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task OnCollectionChanged(bool success)
        {
            if (success)
            {
                await this.OnInitAsync();
            }

            this.StateHasChanged();
        }

        /// <summary>Handler of collection edited.</summary>
        /// <param name="success">Value indicating whether the collection is edited.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task OnEditionCompleted(bool success)
        {
            if (success)
            {
                await this.OnInitAsync();
            }

            this.IsSerieEditionEnabled = new KeyValuePair<int, bool>(0, false);
            this.StateHasChanged();
        }

        /// <summary>
        ///     Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree. Override this method if you will perform
        ///     an asynchronous operation and want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");

            // Apply search filter if any
            this.ApplySearchFilter();

            // Get number of pages
            this.TotalPages = (int)Math.Ceiling(this.filteredComicSerie.Count() / (decimal)PageSize);
            this.ShowPage(this.CurrentPage);
        }

        /// <summary>Method of page display.</summary>
        /// <param name="pageToShow">Page number to display.</param>
        protected void ShowPage(int pageToShow)
        {
            this.ViewableComicSerie = this.filteredComicSerie.Skip((pageToShow - 1) * PageSize).Take(PageSize);
            this.CurrentPage = pageToShow;

            this.PagerMin = pageToShow - PagerSize <= 0 ? 1 : pageToShow - PagerSize;
            this.PagerMax = pageToShow + PagerSize >= this.TotalPages ? this.TotalPages : pageToShow + PagerSize;

            this.StateHasChanged();
        }

        /// <summary>Method of displaying data sorted by issue count.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task SortSeriesByIssueCount()
        {
            if (this.asc)
            {
                this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie?sortBy=IssuesCount desc");
                this.asc = false;
            }
            else
            {
                this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie?sortBy=IssuesCount asc");
                this.asc = true;
            }

            // Apply search filter if any
            this.ApplySearchFilter();

            // Get number of pages
            this.TotalPages = (int)Math.Ceiling(this.filteredComicSerie.Count() / (decimal)PageSize);
            this.ShowPage(this.CurrentPage);
        }

        /// <summary>Method of displaying data sorted by name.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task SortSeriesByName()
        {
            if (this.asc)
            {
                this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie?sortBy=name desc");
                this.asc = false;
            }
            else
            {
                this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie?sortBy=name asc");
                this.asc = true;
            }

            // Apply search filter if any
            this.ApplySearchFilter();

            // Get number of pages
            this.TotalPages = (int)Math.Ceiling(this.filteredComicSerie.Count() / (decimal)PageSize);
            this.ShowPage(this.CurrentPage);
        }
    }
}
