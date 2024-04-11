using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;

namespace PedroMoreira.Domain.Authentication.Entities
{
    public sealed class ProjectSecurityRole : Entity<ProjectSecurityRoleId>, IAuditableEntity, ISoftDeleteEntity
    {

        /// <summary>
        /// Initialize an instance of <see cref="ProjectSecurityRole"/>
        /// </summary>
        /// <param name="id">Identifier of <see cref="ProjectSecurityRole"/></param>
        /// <param name="projectSecurityId">Identifier of <see cref="ProjectSecurity"/></param>
        /// <param name="value">Identifier of Role</param>
        private ProjectSecurityRole(ProjectSecurityRoleId id, ProjectSecurityId projectSecurityId, string value)
            : base(id) 
        {
            ProjectSecurityId = projectSecurityId;
            Value = value;
        }
        public ProjectSecurityId ProjectSecurityId { get; set; }

        public string Value { get; set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool Deleted { get; }

        public ProjectSecurity ProjectSecurity { get; set; }


        public static ProjectSecurityRole Create(ProjectSecurityRoleId id,
                                                 ProjectSecurityId projectSecurityId,
                                                 string value)
        {
            return new ProjectSecurityRole(id,
                                           projectSecurityId,
                                           value);
        }
    }
}
