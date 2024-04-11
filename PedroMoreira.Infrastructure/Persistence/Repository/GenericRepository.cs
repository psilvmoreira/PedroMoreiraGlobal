using Microsoft.EntityFrameworkCore;
using PedroMoreira.Application.Common.Abstractions.Data;
using PedroMoreira.Domain.Common.Primitives;
using System.Linq.Expressions;

namespace PedroMoreira.Infrastructure.Persistence.Repository
{
    internal abstract class GenericRepository<TEntity, Tkey> 
        where TEntity : Entity<Tkey> 
    {

        protected GenericRepository(PostgresContext context)
        {
            dbContext = context;
        }

        protected IDbContext dbContext {  get; }

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The <typeparamref name="TEntity"/> with the specified identifier if it exists, otherwise null.</returns>
        public Task<TEntity?> GetBydIdAsync<TEntity>(Tkey id)
            where TEntity : Entity<Tkey> => dbContext.GetBydIdAsync<TEntity, Tkey>(id);

        /// <summary>
        /// Inserts the specified entity into the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be inserted into the database.</param>
        public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity<Tkey> => dbContext.Insert<TEntity, Tkey>(entity);

        /// <summary>
        /// Inserts the specified entities into the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to be inserted into the database.</param>
        public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity<Tkey> => dbContext.InsertRange<TEntity, Tkey>(entities);

        /// <summary>
        /// Removes the specified entity from the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be removed from the database.</param>
        public void Remove<TEntity>(TEntity entity)
            where TEntity : Entity<Tkey> => dbContext.Remove<TEntity, Tkey>(entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await dbContext.Set<TEntity, Tkey>().FirstOrDefaultAsync(predicate, cancellationToken);

    }
}
