using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class IndulgenceConfiguration : IEntityTypeConfiguration<Indulgence>
    {
        public void Configure(EntityTypeBuilder<Indulgence> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.User)
                .WithMany(u => u.Indulgences)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
