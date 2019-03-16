namespace AlmaIt.Dotnet.Heroes.Client.ViewModel.Enumeration
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Enum for type of bootstrap alert.
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// Success alert.
        /// </summary>
        [Display(Description = "success")]
        Success = 0,

        /// <summary>
        /// Info alert </summary>
        [Display(Description = "info")]
        Info = 1,

        /// <summary>
        /// Warning alert.
        /// </summary>
        [Display(Description = "warning")]
        Warning = 2,

        /// <summary>
        /// Danger alert.
        /// </summary>
        [Display(Description = "danger")]
        Danger = 3,
    }
}
