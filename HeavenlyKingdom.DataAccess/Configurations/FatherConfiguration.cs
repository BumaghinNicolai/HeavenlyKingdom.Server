using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class FatherConfiguration : IEntityTypeConfiguration<Father>
    {
        public void Configure(EntityTypeBuilder<Father> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(f => f.User)
                .WithOne(u => u.Father)
                .HasForeignKey<Father>(f => f.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
