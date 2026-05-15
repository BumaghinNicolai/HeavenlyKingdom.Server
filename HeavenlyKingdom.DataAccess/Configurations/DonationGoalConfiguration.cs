using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class DonationGoalConfiguration : IEntityTypeConfiguration<DonationGoal>
    {
        public void Configure(EntityTypeBuilder<DonationGoal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Target).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Current).HasColumnType("decimal(18,2)");
        }
    }
}
