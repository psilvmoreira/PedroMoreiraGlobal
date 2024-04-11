using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PedroMoreira.Domain.Common.Primitives;

namespace PedroMoreira.Application.Common.Abstractions.Data
{
    /// <summary>
    /// Represents the application database context interface.
    /// </summary>
    public interface IDbContext
    {

        /// <summary>
        /// Gets the database set for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The database set for the specified entity type.</returns>
        DbSet<TEntity> Set<TEntity, Tkey>()
            where TEntity : Entity<Tkey>;

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The <typeparamref name="TEntity"/> with the specified identifier if it exists, otherwise null.</returns>
        Task<TEntity?> GetBydIdAsync<TEntity,Tkey>(Tkey id)
            where TEntity : Entity<Tkey>;

        /// <summary>
        /// Inserts the specified entity into the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be inserted into the database.</param>
        void Insert<TEntity, Tkey>(TEntity entity)
            where TEntity : Entity<Tkey>;

        /// <summary>
        /// Inserts the specified entities into the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to be inserted into the database.</param>
        void InsertRange<TEntity, Tkey>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity<Tkey>;

        /// <summary>
        /// Removes the specified entity from the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be removed from the database.</param>
        void Remove<TEntity, Tkey>(TEntity entity)
            where TEntity : Entity<Tkey>;

        /// <summary>
        /// Executes the specified SQL command asynchronously and returns the number of affected rows.
        /// </summary>
        /// <param name="sql">The SQL command.</param>
        /// <param name="parameters">The parameters collection.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);
    }
}
