namespace PedroMoreira.Domain.Common.Primitives
{
    /// <summary>
    /// Represents the aggregate root.
    /// </summary>
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        protected AggregateRoot(TKey id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected AggregateRoot() : base() { }

        // TODO: Add Domain events
    }
}
