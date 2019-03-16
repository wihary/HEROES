namespace AlmaIt.Dotnet.Heroes.Client.ViewModel.Page
{
    using Microsoft.AspNetCore.Blazor.Components;

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
    }
}