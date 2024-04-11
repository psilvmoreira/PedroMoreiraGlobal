namespace PedroMoreira.Domain.Common.Abstractions
{
    /// <summary>
    ///  Represents the marker interface for soft-deleted entities.
    /// </summary>
    public interface ISoftDeleteEntity
    {
        /// <summary>
        /// Gets the date and time in UTC format the entity was deleted on.
        /// </summary>
        DateTime? DeletedOnUtc { get; }

        /// <summary>
        /// Gets a value indicating whether the entity has been deleted.
        /// </summary>
        bool Deleted { get; }
    }
}
