#pragma warning disable SA1401 // Fields should be private
namespace AlmaIt.Dotnet.Heroes.Client.Pages.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.AspNetCore.Blazor.Components;

    using Newtonsoft.Json;

    /// <summary>
    /// Class of tag manager page.
    /// </summary>
    public class TagManagerBase : BlazorComponent
    {
        /// <summary>
        /// The alert level.
        /// </summary>
        protected AlertType level = AlertType.Info;

        /// <summary>
        /// Gets the alert message.
        /// </summary>
        protected string Message { get; private set; }

        /// <summary>
        /// Gets the tags list.
        /// </summary>
        protected List<ObjectTag> Tags { get; private set; } = new List<ObjectTag>();

        [Inject]
        private HttpClient Http { get; set; }

        /// <summary>Method for delete a tag by id.</summary>
        /// <param name="id">Tag's is.</param>
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
                this.level = AlertType.Danger;
                this.Message = "Error occured while deleting tag";
                return;
            }

            this.level = AlertType.Success;
            this.Message = "Tag sucessfully deleted";

            await this.OnInitAsync();
            this.StateHasChanged();
        }

        /// <summary>
        /// Method call when the tags collection changed.
        /// </summary>
        /// <param name="success">A value indicating whether a tag was successfully added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task OnCollectionChanged(bool success)
        {
            if (success)
            {
                this.level = AlertType.Success;
                this.Message = "Tag sucessfully created";
            }
            else
            {
                this.level = AlertType.Danger;
                this.Message = "Error occured while creating new tag";
            }

            // reload tag lists
            await this.OnInitAsync().ConfigureAwait(false);

            this.StateHasChanged();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
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
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            this.Tags = serializer.Deserialize<List<ObjectTag>>(jsonReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.level = AlertType.Danger;
                this.Message = ex.Message;
            }
        }
    }
}
#pragma warning restore SA1401 // Fields should be private
