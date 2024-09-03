using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;

namespace PedroMoreira.Domain.Authentication.Entities
{
    public sealed class ProjectSecurityClaim : Entity<ProjectSecurityClaimId>, IAuditableEntity
    {
        /// <summary>
        /// Initialize an instance of <see cref="ProjectSecurityClaim"/>
        /// </summary>
        /// <remarks>Required by EF Core.</remarks>
        private ProjectSecurityClaim()
        {}

        /// <summary>
        /// Initialize an instance of <see cref="ProjectSecurityClaim"/>
        /// </summary>
        /// <param name="id">Identifier of <see cref="ProjectSecurityClaim"/></param>
        /// <param name="projectSecurityId">Identifier of <see cref="ProjectSecurity"/></param>
        /// <param name="claimValue">Claim Value</param>
        /// <param name="claimType">Claim Type</param>
        private ProjectSecurityClaim(ProjectSecurityClaimId id,
                                     ProjectSecurityId projectSecurityId,
                                     string claimValue,
                                     string claimType) : 
            base(id)
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

        public static ProjectSecurityClaim Create(
            ProjectSecurityClaimId id, 
            ProjectSecurityId projectSecurityId,
            string value, 
            string type)
        {
            return new ProjectSecurityClaim(
                id, 
                projectSecurityId,
                value, 
                type);
        }
    }
}
