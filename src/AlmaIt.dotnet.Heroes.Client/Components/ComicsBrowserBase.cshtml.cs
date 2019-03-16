namespace AlmaIt.Dotnet.Heroes.Client.Components
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Business;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    /// <summary>Class representing the comics blazor component of the comics.</summary>
    public class ComicsBrowserBase : BlazorComponent
    {
        private const int PagerSize = 5;

        private const int PageSize = 10;

        private bool asc = true;

        private bool overrideDiplayAllComics;

        /// <summary>Gets this current page.</summary>
        protected int CurrentPage { get; private set; } = 1;

        /// <summary>Gets or sets the edition status.</summary>
        protected KeyValuePair<int, bool> EditionStatus { get; set; } = new KeyValuePair<int, bool>(0, false);

        /// <summary>Gets or sets the filter.</summary>
        protected string Filter { get; set; } = string.Empty;

        /// <summary>Gets or sets the override toggle message.</summary>
        protected string OverrideToggleMessage { get; set; } = "Show all";

        /// <summary>Gets or sets ths pager max.</summary>
        protected int PagerMax { get; set; } = 1;

        /// <summary>Gets or sets the pager min.</summary>
        protected int PagerMin { get; set; } = 1;

        /// <summary>Gets or sets the statistics.</summary>
        protected string Statistics { get; set; } = string.Empty;

        /// <summary>Gets or sets the total pages.</summary>
        protected int TotalPages { get; set; } = 1;

        /// <summary>Gets or sets the total result.</summary>
        protected int TotalResult { get; set; }

        /// <summary>Gets or sets the viewable comic book.</summary>
        protected IEnumerable<ComicBook> ViewableComicBooks { get; set; } = new List<ComicBook>();

        /// <summary>Gets or sets the browser type.</summary>
        [Parameter]
        private ComicsBrowserType BrowserType { get; set; }

        /// <summary>Gets or sets the http client.</summary>
        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>Method of reloading data.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public async Task ReloadDataList()
        {
            await this.OnInitAsync().ConfigureAwait(false);
            this.StateHasChanged();
        }

        /// <summary>
        ///     Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree. Override this method if you will perform
        ///     an asynchronous operation and want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync()
        {
            try
            {
                await this.ShowPage(this.CurrentPage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.Statistics = ex.ToString();
            }

            this.StateHasChanged();
        }

        /// <summary>Method of page display.</summary>
        /// <param name="pageToShow">Page number to display.</param>
        /// <param name="sortBy">Name of the sort column.</param>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task ShowPage(int pageToShow, string sortBy = "")
        {
            this.CurrentPage = pageToShow;
            await this.GetCurrentPageData(this.BrowserType, this.CurrentPage, PageSize, sortBy, this.Filter).ConfigureAwait(false);

            this.PagerMin = pageToShow - PagerSize <= 0 ? 1 : pageToShow - PagerSize;
            this.PagerMax = pageToShow + PagerSize >= this.TotalPages ? this.TotalPages : pageToShow + PagerSize;

            this.StateHasChanged();
        }

        /// <summary>Filter application method.</summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task ApplySearchFilter()
        {
            this.CurrentPage = 1;
            await this.ShowPage(this.CurrentPage).ConfigureAwait(false);
            this.StateHasChanged();
        }

        /// <summary>Editing method of a comic.</summary>
        /// <param name="id">Comic's id.</param>
        protected void EditComicBook(int id)
        {
            this.EditionStatus = new KeyValuePair<int, bool>(id, true);

            this.StateHasChanged();
        }

        /// <summary>Method of removing a comic.</summary>
        /// <param name="id">Comic's id.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task DeleteComicBook(int id)
        {
            var responseMessage = await this.Http.DeleteAsync($"/api/ComicBook/{id}").ConfigureAwait(false);
            await this.OnInitAsync().ConfigureAwait(false);
            Console.WriteLine($"Delete OK ? : {responseMessage.IsSuccessStatusCode}");
            this.StateHasChanged();
        }

        /// <summary>Handler of collection changed.</summary>
        /// <param name="success">Value indicating whether the collection has changed.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        protected async Task OnCollectionChanged(bool success)
        {
            if (success)
            {
                await this.OnInitAsync().ConfigureAwait(false);
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
                await this.OnInitAsync().ConfigureAwait(false);
            }

            this.EditionStatus = new KeyValuePair<int, bool>(0, false);

            this.StateHasChanged();
        }

        /// <summary>Method of displaying data sorted by issue number.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task SortSeriesByIssueNumber()
        {
            await this.ShowPage(this.CurrentPage, $"IssueNumber {(this.asc ? "asc" : "desc")}").ConfigureAwait(false);
            this.asc = !this.asc;
        }

        /// <summary>Method of displaying data sorted by serie name.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task SortSeriesBySerieName()
        {
            await this.ShowPage(this.CurrentPage, $"ComicSerie.Name {(this.asc ? "asc" : "desc")}").ConfigureAwait(false);
            this.asc = !this.asc;
        }

        /// <summary>Method of displaying data sorted by title.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task SortSeriesByTitle()
        {
            await this.ShowPage(this.CurrentPage, $"title {(this.asc ? "asc" : "desc")}").ConfigureAwait(false);
            this.asc = !this.asc;
        }

        /// <summary>Overload method of the browser type.</summary>
        /// <returns>A <see cref="Task" /> representing the result of the asynchronous operation.</returns>
        protected async Task ToggleOverride()
        {
            this.overrideDiplayAllComics = !this.overrideDiplayAllComics;
            if (this.overrideDiplayAllComics)
            {
                this.OverrideToggleMessage = $"Default filter ({this.BrowserType})";
                this.BrowserType = ComicsBrowserType.All;
            }
            else
            {
                this.OverrideToggleMessage = "Show all";
                this.BrowserType = ComicsBrowserType.Collection;
            }

            await this.OnInitAsync().ConfigureAwait(false);
            this.StateHasChanged();
        }

        /// <summary>Method for obtaining data for one page.</summary>
        /// <param name="browserType">Type of browser.</param>
        /// <param name="pageToShow">Page number to display.</param>
        /// <param name="size">Page size to display.</param>
        /// <param name="sortBy">Name of the sort column.</param>
        /// <param name="filter">Filter of the data display.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        private async Task GetCurrentPageData(ComicsBrowserType browserType, int pageToShow, int size, string sortBy, string filter = "")
        {
            Stream response;

            if (browserType == ComicsBrowserType.All)
            {
                response = await this.Http.GetStreamAsync($"api/ComicBook/{pageToShow}/{size}/{filter}{(string.IsNullOrEmpty(sortBy) ? string.Empty : $"?sortBy={sortBy}")}").ConfigureAwait(false);
            }
            else
            {
                response = await this.Http.GetStreamAsync(
                               $"api/ComicBook/type/{browserType}/{pageToShow}/{size}/{filter}{(string.IsNullOrEmpty(sortBy) ? string.Empty : $"?sortBy={sortBy}")}").ConfigureAwait(false);
            }

            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                if (response.Length > 0)
                {
                    var serializer = new JsonSerializer();
                    using (var sr = new StreamReader(response))
                    {
                        using (var jsonTextReader = new JsonTextReader(sr))
                        {
                            var serviceData = serializer.Deserialize<PageResponseData<ComicBook>>(jsonTextReader);
                            this.ViewableComicBooks = serviceData.Result;
                            this.TotalPages = serviceData.MaxPage;
                            this.TotalResult = serviceData.TotalResult;
                        }
                    }
                }

                stopWatch.Stop();
                this.Statistics = $"{stopWatch.ElapsedMilliseconds.ToString()}ms";
            }
            catch (Exception ex)
            {
                this.Statistics = ex.ToString();
            }

            this.StateHasChanged();
        }
    }
}
