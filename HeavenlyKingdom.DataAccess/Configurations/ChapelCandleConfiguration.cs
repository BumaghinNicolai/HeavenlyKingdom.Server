using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class ChapelCandleConfiguration : IEntityTypeConfiguration<ChapelCandle>
    {
        public void Configure(EntityTypeBuilder<ChapelCandle> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(c => c.User)
                .WithMany(u => u.ChapelCandles)
                .HasForeignKey(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
