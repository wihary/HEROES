namespace AlmaIt.dotnet.Heroes.Client.Pages.Components
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;

    #endregion

    public class SeriesBrowserBase : BlazorComponent
    {
        private const int PagerSize = 5;

        private const int PageSize = 10;

        protected string Filter = string.Empty;

        private bool asc = true;

        protected int CurrentPage = 1;

        private IEnumerable<ComicSeries> filteredComicSerie = new List<ComicSeries>();

        protected List<ComicSeries> FullComicSerieList;

        protected KeyValuePair<int, bool> IsSerieEditionEnabled = new KeyValuePair<int, bool>(0, false);

        protected int PagerMax = 1;

        protected int PagerMin = 1;

        protected int TotalPages = 1;

        protected IEnumerable<ComicSeries> ViewableComicSerie = new List<ComicSeries>();

        [Inject]
        private HttpClient Http { get; set; }

        [Parameter]
        private Action<string> SendMessage { get; set; }

        public async Task ReloadDataList()
        {
            await this.OnInitAsync();
            this.StateHasChanged();
        }

        protected void ApplySearchFilter()
        {
            this.filteredComicSerie =
                string.IsNullOrEmpty(this.Filter)
                    ? this.FullComicSerieList
                    : this.FullComicSerieList.Where(serie => serie.Name.Contains(this.Filter)).ToList();

            this.StateHasChanged();
            this.ShowPage(this.CurrentPage);
        }

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

        protected void EditComicSerie(int id)
        {
            this.IsSerieEditionEnabled = new KeyValuePair<int, bool>(id, true);

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

            this.IsSerieEditionEnabled = new KeyValuePair<int, bool>(0, false);
            this.StateHasChanged();
        }


        protected override async Task OnInitAsync()
        {
            this.FullComicSerieList = await this.Http.GetJsonAsync<List<ComicSeries>>("api/ComicSerie");

            // Apply search filter if any
            this.ApplySearchFilter();

            // Get number of pages
            this.TotalPages = (int)Math.Ceiling(this.filteredComicSerie.Count() / (decimal)PageSize);
            this.ShowPage(this.CurrentPage);
        }

        protected void ShowPage(int pageToShow)
        {
            this.ViewableComicSerie = this.filteredComicSerie.Skip((pageToShow - 1) * PageSize).Take(PageSize);
            this.CurrentPage = pageToShow;

            this.PagerMin = pageToShow - PagerSize <= 0 ? 1 : pageToShow - PagerSize;
            this.PagerMax = pageToShow + PagerSize >= this.TotalPages ? this.TotalPages : pageToShow + PagerSize;

            this.StateHasChanged();
        }

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
