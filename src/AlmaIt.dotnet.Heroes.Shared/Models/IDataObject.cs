using System;

namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    /// <summary>
    /// Base interface defintion of an object model.
    /// </summary>
    /// <typeparam name="TIdentity">primary key type</typeparam>
    public interface IDataObject<TIdentity>
        where TIdentity : IComparable<TIdentity>
    {
        /// <summary>
        /// Get or Set primary key
        /// </summary>
        TIdentity Id { get; set; }
    }
}