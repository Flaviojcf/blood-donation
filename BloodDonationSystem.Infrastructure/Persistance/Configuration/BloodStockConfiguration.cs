using BloodDonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationSystem.Infrastructure.Persistance.Configuration
{
    public class BloodStockConfiguration : IEntityTypeConfiguration<BloodStock>
    {
        public void Configure(EntityTypeBuilder<BloodStock> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(b => b.BloodType)
                   .IsRequired();

            builder.Property(b => b.RhFactorType)
                   .IsRequired();

            builder.Property(b => b.QuantityML)
                   .IsRequired();

            builder.Property(b => b.MinQuantityML)
                   .IsRequired();
        }
    }
}
