namespace AlmaIt.Dotnet.Heroes.Client.ViewModel.Page
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Newtonsoft.Json;

    /// <summary>
    ///     Code-behind of the Comic Series Overview Page
    /// </summary>
    public class ComicSerieOverviewBase : ViewModelBase
    {
        /// <summary>
        ///     Navigation parameter that gives the Comic Serie ID use to rendre the overview
        /// </summary>
        [Parameter]
        public string ComicSerieId { get; set; }

        /// <summary>
        ///     
        /// </summary>
        public ComicSeries ComicBookSerie { get; set; }

        /// <summary>
        ///     Method that get called once parameter has been set
        /// </summary>
        protected async override Task OnParametersSetAsync()
        {
            if(!string.IsNullOrEmpty(this.ComicSerieId))
                await this.LoadComicBookSerieAsync();
        }

        /// <summary>
        ///     Method dedicated to comic serie detail load
        /// </summary>
        private async Task LoadComicBookSerieAsync()
        {
             try
            {
                var response = await this.Http.GetAsync($"/api/ComicSerie/{this.ComicSerieId}");

                if (response == null || !response.IsSuccessStatusCode)
                {
                    this.Message = $"Erreur de récupération : [{response.StatusCode}]{await response.Content.ReadAsStringAsync()}";
                    this.Type = AlertType.Warning;
                }
                else
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();

                    if (responseStream?.Length > 0)
                    {
                        var serializer = new JsonSerializer();
                        using (var sr = new StreamReader(responseStream))
                        using (var jsonTextReader = new JsonTextReader(sr))
                        {
                            this.ComicBookSerie = serializer.Deserialize<ComicSeries>(jsonTextReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Message = $"Erreur de récupération : {ex.Message}";
                this.Type = AlertType.Danger;
                Console.WriteLine($"Exception occurred : {ex}");
            }

            this.StateHasChanged();
        }
    }
}