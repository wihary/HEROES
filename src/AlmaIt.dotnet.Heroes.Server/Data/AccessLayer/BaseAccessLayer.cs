namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Interface defining base CRUD operation on model data.
    /// </summary>
    /// <typeparam name="TContext">Data context that is using this implementation.</typeparam>
    /// <typeparam name="TModel">Object Model that is using this implementation.</typeparam>
    /// <typeparam name="TIdentity">Primary key type.</typeparam>
    internal abstract class BaseAccessLayer<TContext, TModel, TIdentity> : IBaseAccessLayer<TModel, TIdentity>
        where TModel : class, IDataObject<TIdentity>
        where TIdentity : IComparable<TIdentity>
        where TContext : DbContext
    {
        /// <summary>
        /// Get Db context.
        /// </summary>
        private readonly TContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAccessLayer{TContext, TModel, TIdentity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseAccessLayer(TContext context)
        {
            this.context = context;
            this.ModelSet = this.context.Set<TModel>();
        }

        /// <summary>
        /// Gets the dataset for implemented data model object.
        /// </summary>
        protected DbSet<TModel> ModelSet { get; }

        /// <summary>
        ///     Async Method that add new object in Db.
        /// </summary>
        /// <param name="model">Object model to add.</param>
        /// <returns>Returns Id of newly created data object</returns>
        public virtual async Task<TIdentity> AddAsync(TModel model)
        {
            var result = this.ModelSet.Add(model);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return result.Entity.Id;
        }

        /// <summary>
        ///     Async Method that add a range of new object in Db.
        /// </summary>
        /// <param name="models">Enumerable of objects model to add.</param>
        /// <returns>Returns number of state entries written to the database</returns>
        public virtual async Task<int> AddRangeAsync(IEnumerable<TModel> models)
        {
            this.ModelSet.AddRange(models);
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Method that verify if object exists based on its primary key.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Return true if object exists in db.</returns>
        public bool Exists(TIdentity id) => this.ModelSet.Any(e => e.Id.CompareTo(id) == 0);

        /// <summary>
        ///     Async Method that retrieve data based on primary key.
        /// </summary>
        /// <param name="id">Primary key.</param>
        /// <returns>Returns <typeparamref name="TModel"/>.</returns>
        public virtual async Task<TModel> GetAsync(TIdentity id)
            => await this.ModelSet.SingleOrDefaultAsync(model => model.Id.CompareTo(id) == 0).ConfigureAwait(false);

        /// <summary>
        ///     Async Method that return all data object existing in Db.
        /// </summary>
        /// <returns>List of <typeparamref cref="TModel"/>.</returns>
        public virtual IAsyncEnumerable<TModel> GetAllAsync() => this.ModelSet.ToAsyncEnumerable();

        /// <summary>
        ///     Async Method that remove a specific object in Db.
        /// </summary>
        /// <param name="model">The object data model to remove.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        public async Task<int> RemoveAsync(TModel model)
        {
            this.ModelSet.Remove(model);
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Async method using bulk deletion method to remove data object from db context.
        /// </summary>
        /// <param name="models">Enumerable of Data object to remove.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        public async Task<int> RemoveRangeAsync(IEnumerable<TModel> models)
        {
            this.ModelSet.RemoveRange(models);
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Async method using bulk deletion method to remove all objects from db context.
        /// </summary>
        /// <returns>Returns number of state entries written to the database.</returns>
        public async Task<int> RemoveAllAsync()
        {
            this.ModelSet.RemoveRange(this.GetAllAsync().ToEnumerable());
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Async method that update a specific data object.
        /// </summary>
        /// <param name="model">The object data model to update.</param>
        /// <returns>Returns number of state entries written to the database.</returns>
        public async Task<int> UpdateAsync(TModel model)
        {
            this.ModelSet.Update(model);
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Method that retrieve data based on Filter Expression as parameter.
        /// </summary>
        /// <param name="filters">Linq expression use to filter the data.</param>
        /// <returns>Return Queryable of <see cref="TModel"/>.</returns>
        public IQueryable<TModel> Where(Expression<Func<TModel, bool>> filters)
            => this.ModelSet.Where(filters);
    }
}