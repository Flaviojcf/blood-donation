using BloodDonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationSystem.Infrastructure.Persistance.Configuration
{
    public class DonorConfiguration : IEntityTypeConfiguration<Donor>
    {
        public void Configure(EntityTypeBuilder<Donor> builder)
        {
            builder.ToTable("Donor");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(d => d.FullName).IsRequired();
            builder.Property(d => d.Email).IsRequired();
            builder.Property(d => d.Password).IsRequired();
            builder.Property(d => d.BirthDate).IsRequired();
            builder.Property(d => d.GenderType).IsRequired();
            builder.Property(d => d.Weight).IsRequired();
            builder.Property(d => d.BloodType).IsRequired();
            builder.Property(d => d.RhFactorType).IsRequired();

            builder.HasIndex(d => d.Email).IsUnique();
        }
    }
}
