using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
    {
        public void Configure(EntityTypeBuilder<ServiceOrder> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(so => so.User)
                .WithMany()
                .HasForeignKey(so => so.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(so => so.Father)
                .WithMany(f => f.ServiceOrders)
                .HasForeignKey(so => so.FatherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
