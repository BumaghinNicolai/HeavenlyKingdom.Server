using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
