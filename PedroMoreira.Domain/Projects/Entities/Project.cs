using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Projects.ValueObjects;

namespace PedroMoreira.Domain.Projects.Entities
{
    public class Project : Entity<ProjectId>, IAuditableEntity, ISoftDeleteEntity
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool Deleted { get; }
        public IEnumerable<ProjectSecurity> ProjectSecurity { get; set; }
    }
}
