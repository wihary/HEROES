namespace AlmaIt.dotnet.Heroes.Shared.Business
{
    using System.Collections.Generic;

    /// <summary>
    /// Data transfert class for paged result.
    /// </summary>
    /// <typeparam name="T">Type of result.</typeparam>
    public class PageResponseData<T>
    {
        /// <summary>
        /// Gets or sets the max page.
        /// </summary>
        public int MaxPage { get; set; }

        /// <summary>
        /// Gets or sets the total result.
        /// </summary>
        public int TotalResult { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public IEnumerable<T> Result { get; set; }
    }
}