using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Domain.Members.Entities
{
    public sealed class Member : Entity<MemberId>, IAuditableEntity, ISoftDeleteEntity
    {

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime LockoutEnd { get; set; }

        public int AccessFailedCount { get; set; }

        public MemberProfile Profile { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool Deleted { get; }

        public ICollection<ProjectSecurity> ProjectSecurity { get; set; }

        public ICollection<MemberToken> UserTokens { get; set; }

    }
}
