using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Projects.Entities;
using PedroMoreira.Domain.Projects.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(prop => prop.Id)
                .HasConversion(
                    id => id.Value,
                    value => ProjectId.Create(value).Value);
            
            builder
                .HasMany(projeSec => projeSec.ProjectSecurity)
                .WithOne(proj => proj.Project)
                .HasForeignKey(forkey => forkey.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
