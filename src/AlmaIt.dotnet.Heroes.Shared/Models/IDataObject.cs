namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System;

    /// <summary>Base interface defintion of an object model.</summary>
    /// <typeparam name="TIdentity">Unique identifier (primary key).</typeparam>
    public interface IDataObject<TIdentity>
        where TIdentity : IComparable<TIdentity>
    {
        /// <summary>Gets or sets the primary key.</summary>
        TIdentity Id { get; set; }
    }
}
