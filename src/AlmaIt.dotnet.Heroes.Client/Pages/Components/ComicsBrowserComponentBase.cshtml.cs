namespace AlmaIt.dotnet.Heroes.Client.Pages.Components
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Business;
    using AlmaIt.dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    public class ComicsBrowserComponentBase : BlazorComponent
    {
        private bool asc = true;

        protected int CurrentPage = 1;

        protected KeyValuePair<int, bool> EditionStatus = new KeyValuePair<int, bool>(0, false);

        protected string Filter = string.Empty;

        private bool overrideDiplayAllComics;

        protected string OverrideToggleMessage = "Show all";

        protected int PagerMax = 1;

        protected int PagerMin = 1;

        private const int PagerSize = 5;

        private const int PageSize = 10;

        protected string Statistics = string.Empty;

        protected int TotalPages = 1;

        protected int TotalResult;

        protected IEnumerable<ComicBook> ViewableComicBooks = new List<ComicBook>();

        [Parameter]
        private ComicsBrowserType BrowserType { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        public async Task ReloadDataList()
        {
            await this.OnInitAsync();
            this.StateHasChanged();
        }

        protected async Task ApplySearchFilter()
        {
            this.CurrentPage = 1;
            await this.ShowPage(this.CurrentPage);
            this.StateHasChanged();
        }

        protected async Task DeleteComicBook(int id)
        {
            var result = await this.Http.DeleteAsync($"/api/ComicBook/{id}");
            await this.OnInitAsync();

            this.StateHasChanged();
        }

        protected void EditComicBook(int id)
        {
            this.EditionStatus = new KeyValuePair<int, bool>(id, true);

            this.StateHasChanged();
        }


        protected async Task GetCurrentPageData(ComicsBrowserType browserType, int pageToShow, int size, string sortBy, string filter = "")
        {
            Stream response = null;

            switch (browserType)
            {
                case ComicsBrowserType.All:
                    response = await this.Http.GetStreamAsync($"api/ComicBook/{pageToShow}/{size}/{filter}{(string.IsNullOrEmpty(sortBy) ? "" : $"?sortBy={sortBy}")}");
                    break;
                default:
                    response = await this.Http.GetStreamAsync($"api/ComicBook/type/{browserType}/{pageToShow}/{size}/{filter}{(string.IsNullOrEmpty(sortBy) ? "" : $"?sortBy={sortBy}")}");
                    break;
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

        protected async Task OnCollectionChanged(bool success)
        {
            if (success)
            {
                await this.OnInitAsync();
            }

            this.StateHasChanged();
        }

        protected async Task OnEditionCompleted(bool success)
        {
            if (success)
            {
                await this.OnInitAsync();
            }

            this.EditionStatus = new KeyValuePair<int, bool>(0, false);

            this.StateHasChanged();
        }

        protected override async Task OnInitAsync()
        {
            try
            {
                await this.ShowPage(this.CurrentPage);
            }
            catch (Exception ex)
            {
                this.Statistics = ex.ToString();
            }

            this.StateHasChanged();
        }

        protected async Task ShowPage(int pageToShow, string sortBy = "")
        {
            this.CurrentPage = pageToShow;
            await this.GetCurrentPageData(this.BrowserType, this.CurrentPage, PageSize, sortBy, this.Filter);

            this.PagerMin = pageToShow - PagerSize <= 0 ? 1 : pageToShow - PagerSize;
            this.PagerMax = pageToShow + PagerSize >= this.TotalPages ? this.TotalPages : pageToShow + PagerSize;

            this.StateHasChanged();
        }

        protected async Task SortSeriesByIssueNumber()
        {
            await this.ShowPage(this.CurrentPage, $"IssueNumber {(this.asc ? "asc" : "desc")}");
            this.asc = !this.asc;
        }

        protected async Task SortSeriesBySerieName()
        {
            await this.ShowPage(this.CurrentPage, $"ComicSerie.Name {(this.asc ? "asc" : "desc")}");
            this.asc = !this.asc;
        }

        protected async Task SortSeriesByTitle()
        {
            await this.ShowPage(this.CurrentPage, $"title {(this.asc ? "asc" : "desc")}");
            this.asc = !this.asc;
        }

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


            await this.OnInitAsync();
            this.StateHasChanged();
        }
    }
}
