
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PedroMoreira.Application.Common.Abstractions.Data;
using PedroMoreira.Application.Common.Services;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using System.Reflection;

namespace PedroMoreira.Infrastructure.Persistence
{
    public class PostgresContext : DbContext, IDbContext, IUnitofWork
    {
        private readonly IDateTImeProvider _dateTime;

        public PostgresContext(
            DbContextOptions<PostgresContext> options, 
            IDateTImeProvider dateTime
            ) : base(options)
        {
            _dateTime = dateTime;
        }


        /// <inheritdoc/>
        public DbSet<TEntity> Set<TEntity,Tkey>()
            where TEntity : Entity<Tkey>
            => base.Set<TEntity>();

        /// <inheritdoc/>>
        public async Task<TEntity> GetBydIdAsync<TEntity, Tkey>(Tkey id)
            where TEntity : Entity<Tkey>
        {
            return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        /// <inheritdoc/>
        public void Insert<TEntity,Tkey>(TEntity entity)
            where TEntity : Entity<Tkey>
            => Set<TEntity>().Add(entity);

        /// <inheritdoc/>>
        public void InsertRange<TEntity,Tkey>(IReadOnlyCollection<TEntity> entities) 
            where TEntity : Entity<Tkey>
            => Set<TEntity>().AddRange(entities);

        /// <inheritdoc/>
        public void Remove<TEntity,Tkey>(TEntity entity) 
            where TEntity : Entity<Tkey>
            => Set<TEntity>().Remove(entity);

        /// <inheritdoc/>
        public Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
            => Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

        /// <summary>
        /// Saves all of the pending changes in the unit of work.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of entities that have been saved.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            DateTime utcNow = _dateTime.UtcNow;

            UpadateAuditableEntities(utcNow);

            UpadateSoftDeletedEntities(utcNow);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Updates the entities implementing <see cref="IAuditableEntity"/> interface.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        private void UpadateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;

                if (entityEntry.State == EntityState.Added)
                    entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
            }
        }

        /// <summary>
        /// Updates the entities implementing <see cref="ISoftDeletableEntity"/> interface.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        private void UpadateSoftDeletedEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State != EntityState.Deleted)
                    continue;

                    entityEntry.Property(nameof(ISoftDeleteEntity.DeletedOnUtc)).CurrentValue = utcNow;
                    entityEntry.Property(nameof(ISoftDeleteEntity.Deleted)).CurrentValue = true;

                UpdateSoftDeleteEntitiesToUnchanged(entityEntry);
            }
        }

        /// <summary>
        /// Updates the specified entity entry's referenced entries in the deleted state to the modified state.
        /// This method is recursive.
        /// </summary>
        /// <param name="entityEntry">The entity entry.</param>
        private void UpdateSoftDeleteEntitiesToUnchanged(EntityEntry entityEntry)
        {
            if (!entityEntry.References.Any())
                return;

            foreach (ReferenceEntry reference in entityEntry.References)
            {
                reference.TargetEntry.State = EntityState.Unchanged;

                UpdateSoftDeleteEntitiesToUnchanged(reference.TargetEntry);
            }
        }

    }

}