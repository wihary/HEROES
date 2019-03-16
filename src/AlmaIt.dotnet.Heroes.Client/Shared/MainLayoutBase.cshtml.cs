namespace AlmaIt.Dotnet.Heroes.Client.Shared
{
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Client.ViewModel;

    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Layouts;

    /// <summary>
    /// Class of main layout page.
    /// </summary>
    public class MainLayoutBase : BlazorLayoutComponent
    {
        [Inject]
        private AppState AppState { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnInitAsync() => await this.AppState.IsLoggedInAsync();
    }
}
