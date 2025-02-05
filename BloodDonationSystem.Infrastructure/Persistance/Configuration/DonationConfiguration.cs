using BloodDonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationSystem.Infrastructure.Persistance.Configuration
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.ToTable("Donation");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(d => d.DonationDate).IsRequired();
            builder.Property(d => d.QuantityML).IsRequired();


            builder.HasOne(d => d.Donor)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
