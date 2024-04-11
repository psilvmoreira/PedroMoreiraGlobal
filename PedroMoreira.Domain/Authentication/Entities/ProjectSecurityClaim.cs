using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;

namespace PedroMoreira.Domain.Authentication.Entities
{
    public sealed class ProjectSecurityClaim : Entity<ProjectSecurityClaimId>, IAuditableEntity
    {

        private ProjectSecurityClaim(ProjectSecurityClaimId id,
                                     ProjectSecurityId projectSecurityId,
                                     string claimValue,
                                     string claimType) : base(id)
        {
            ProjectSecurityId = projectSecurityId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public ProjectSecurityId ProjectSecurityId { get; set; }

        public string ClaimValue { get; set; }

        public string ClaimType { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public ProjectSecurity ProjectSecurity { get; set; }

        public static ProjectSecurityClaim Create(ProjectSecurityClaimId Id, ProjectSecurityId ProjectSecurityId, string Value, string Type)
        {
            return new ProjectSecurityClaim(
                Id, 
                ProjectSecurityId, 
                Value, 
                Type);
        }
    }
}
