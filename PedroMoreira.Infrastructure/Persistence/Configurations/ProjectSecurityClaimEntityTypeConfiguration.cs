using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Authentication.Entities;
using PedroMoreira.Domain.Authentication.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Configurations;

public class ProjectSecurityClaimEntityTypeConfiguration 
    : IEntityTypeConfiguration<ProjectSecurityClaim>
{
    public void Configure(EntityTypeBuilder<ProjectSecurityClaim> builder)
    {
        
        builder.Property(prop => prop.Id)
            .HasConversion(
                id => id.Value,
                value => ProjectSecurityClaimId.Create(value).Value);
        
        builder
            .HasIndex(projmem => new { projmem.Id });

        builder
            .HasOne(proj => proj.ProjectSecurity)
            .WithMany(projClaim => projClaim.Claims)
            .HasForeignKey(fKey => fKey.ProjectSecurityId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
    }
}