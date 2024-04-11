using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Domain.Members.Entities
{
    public sealed class MemberToken : Entity<int>, IAuditableEntity
    {

        public MemberId MemberId { get; set; }

        public string Value { get; set; }

        public string Token { get; set; }

        public DateTime? Expiration { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public Member Member { get; set; }
    }
}
