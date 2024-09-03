using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(prop => prop.Id)
                .HasConversion(
                    id => id.value,
                    value => MemberId.Create(value).Value);
            
            builder
                .HasOne(MenPro => MenPro.Profile)
                .WithOne(memb => memb.User)
                .HasForeignKey<MemberProfile>(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(Utok => Utok.UserTokens)
                .WithOne(memb => memb.Member)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(projSec => projSec.ProjectSecurity)
                .WithOne(memb => memb.Member)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
