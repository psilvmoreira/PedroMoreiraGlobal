using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Domain.Members.Entities
{

    public sealed class MemberProfile : Entity<int>, IAuditableEntity, ISoftDeleteEntity
    {

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public MemberId MemberId { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool Deleted { get; }

        public Member? User { get; set; }
    }
}
