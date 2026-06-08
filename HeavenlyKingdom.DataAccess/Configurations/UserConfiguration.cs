using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeavenlyKingdom.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            // All relationships are configured in their respective child entity configurations:
            // Address, Notification, Order, Favorite, ChapelCandle, Indulgence, Father
        }
    }
}
