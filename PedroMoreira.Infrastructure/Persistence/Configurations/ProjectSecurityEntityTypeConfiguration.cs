using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Authentication.Aggregates;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class ProjectSecurityEntityTypeConfiguration
        : IEntityTypeConfiguration<ProjectSecurity>
    {
        public void Configure(EntityTypeBuilder<ProjectSecurity> builder)
        {
            builder
                .HasIndex(projMem => new { projMem.ProjectId, projMem.MemberId })
                .IsUnique(true);


            builder
                .HasOne(memb => memb.Member)
                .WithMany(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(proj => proj.Project)
                .WithMany(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasMany(claim => claim.Claims)
                .WithOne(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectSecurityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(roles => roles.Roles)
                .WithOne(proj => proj.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectSecurityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
