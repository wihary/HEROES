namespace AlmaIt.dotnet.Heroes.Client.Pages.Settings
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net;
    using System.Threading.Tasks;
    using System;

    using AlmaIt.dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    public class TagManagerBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        public List<ObjectTag> Tags { get; set; } = new List<ObjectTag>();

        public bool IsErrorMessage { get; set; }

        public string Message { get; set; }

        protected AlertType Level = AlertType.info;

        /// <summary>This method is the entry point to the blazor component rendering</summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            var response = await this.Http.GetAsync("api/tag", HttpCompletionOption.ResponseHeadersRead);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                this.Message = $"[{response.StatusCode}] : {response.ReasonPhrase} ({ex.Message})";
            }

            try
            {
                using(var stream = await response.Content.ReadAsStreamAsync())
                {
                    using(var streamReader = new StreamReader(stream))
                    {
                        using(var jsonReader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            this.Tags = serializer.Deserialize<List<ObjectTag>>(jsonReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.IsErrorMessage = true;
                this.Message = ex.Message;
            }
        }

        protected async Task OnCollectionChanged(bool success)
        {
            if (success)
            {
                this.Level = AlertType.success;
                this.Message = "Tag sucessfully created";
            }
            else
            {
                this.Level = AlertType.danger;
                this.Message = "Error occured while creating new tag";
            }

            // reload tag lists
            await this.OnInitAsync();

            this.StateHasChanged();
        }

        /// <summary>
        /// Method for delete a tag by id.
        /// </summary>
        /// <param name="id">Tag's is</param>
        /// <returns>Retourne une tache.</returns>
        protected async Task DeleteTag(int id)
        {
            var result = await this.Http.DeleteAsync($"/api/tag/{id}");
            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                this.Level = AlertType.danger;
                this.Message = "Error occured while deleting tag";
                return;
            }

            this.Level = AlertType.success;
            this.Message = "Tag sucessfully deleted";

            await this.OnInitAsync();
            this.StateHasChanged();

        }
    }
}
