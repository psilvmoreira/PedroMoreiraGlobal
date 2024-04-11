namespace PedroMoreira.Application.Common.Abstractions.Data
{
    /// <summary>
    /// Represents the application UnitofWork context interface.
    /// </summary>
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
