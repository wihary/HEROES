namespace AlmaIt.dotnet.Heroes.Client.Pages.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Components;
    using Newtonsoft.Json;

    public class TagManagerBase : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }

        public List<ObjectTag> Tags { get; set; }

        public bool IsErrorMessage { get; set; } = false;

        public string Message { get; set; }


        /// <summary>
        ///     This method is the entry point to the blazor component rendering
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitAsync()
        {
            var response = await Http.GetAsync($"api/tag", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        this.Tags = serializer.Deserialize<List<ObjectTag>>(jsonReader);
                    }
                }
                catch (Exception ex)
                {
                    this.IsErrorMessage = true;
                    this.Message = ex.Message;
                }
            }
            else
            {
                this.Message = $"[{response.StatusCode}] : {response.ReasonPhrase}";
            }

            if (this.Tags == null)
                this.Tags = new List<ObjectTag>();
        }
    }
}