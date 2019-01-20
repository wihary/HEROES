namespace AlmaIt.dotnet.Heroes.Client.Shared
{
    using AlmaIt.dotnet.Heroes.Client.ViewModel;
    using Microsoft.AspNetCore.Blazor.Components;

    public class NavMenuBase : BlazorComponent
    {
        [Inject]
        protected AppState AppState { get; set; }
        
        protected bool collapseNavMenu = true;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}