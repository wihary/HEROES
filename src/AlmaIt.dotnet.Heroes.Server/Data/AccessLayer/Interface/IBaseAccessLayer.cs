namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    /// <summary>
    ///     Interface defining base CRUD operation on model data.
    /// </summary>
    /// <typeparam name="TModel">Object Model that is using this implementation.</typeparam>
    /// <typeparam name="TIdentity">Primary key type.</typeparam>
    public interface IBaseAccessLayer<TModel, TIdentity>
        where TModel : class, IDataObject<TIdentity>
        where TIdentity : IComparable<TIdentity>
    {
        /// <summary>
        ///     Async Method that add new object in Db.
        /// </summary>
        /// <param name="model">Object model to add.</param>
        /// <returns>Returns Id of newly created data object.</returns>
        Task<TIdentity> AddAsync(TModel model);

        /// <summary>
        ///     Async Method that add a range of new object in Db.
        /// </summary>
        /// <param name="models">Enumerable of objects model to add.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        Task<int> AddRangeAsync(IEnumerable<TModel> models);

        /// <summary>
        ///     Method that verify if object exists based on its primary key.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Return true if object exists in db.</returns>
        bool Exists(TIdentity id);

        /// <summary>
        ///     Async Method that retrieve data based on primary key.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Returns <typeparamref name="TModel"/>.</returns>
        Task<TModel> GetAsync(TIdentity id);

        /// <summary>
        ///     Async Method that return all data object existing in Db.
        /// </summary>
        /// <returns>List of <typeparamref name="TModel"/>.</returns>
        IAsyncEnumerable<TModel> GetAllAsync();

        /// <summary>
        ///     Async Method that remove a specific object in Db.
        /// </summary>
        /// <param name="model">The object data model to remove.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        Task<int> RemoveAsync(TModel model);

        /// <summary>
        ///     Async method using bulk deletion method to remove data object from db context.
        /// </summary>
        /// <param name="models">Enumerable of Data object to remove.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        Task<int> RemoveRangeAsync(IEnumerable<TModel> models);

        /// <summary>
        ///     Async method using bulk deletion method to remove all objects from db context.
        /// </summary>
        /// <returns>Returns number of state entries written to the database.</returns>
        Task<int> RemoveAllAsync();

        /// <summary>
        ///     Async method that update a specific data object.
        /// </summary>
        /// <param name="model">The object data model to update.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        Task<int> UpdateAsync(TModel model);

        /// <summary>
        ///     Method that retrieve data based on Filter Expression as parameter.
        /// </summary>
        /// <param name="filters">Linq expression use to filter the data.</param>
        /// <returns>Return Queryable of <typeparamref name="TModel"/>.</returns>
        IQueryable<TModel> Where(Expression<Func<TModel, bool>> filters);
    }
}