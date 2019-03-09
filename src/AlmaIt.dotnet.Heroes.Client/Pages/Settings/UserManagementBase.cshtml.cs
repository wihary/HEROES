namespace AlmaIt.Dotnet.Heroes.Client.Pages.Settings
{
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Client.Components.Settings;
    using Microsoft.AspNetCore.Blazor.Components;

    /// <summary>Class represent user management component.</summary>
    public class UserManagementBase : BlazorComponent
    {
        /// <summary>Gets or sets the user list component.</summary>
        protected UserList UserListComponent { get; set; }

        /// <summary>
        /// Method raises a user is successfully added.
        /// </summary>
        /// <param name="success">A value indicating whether the user is successfully added.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task OnUserAdded(bool success)
        {
            if (success)
            {
                await this.UserListComponent.RefreshUserListAsync();
            }

            this.StateHasChanged();
        }
    }
}
