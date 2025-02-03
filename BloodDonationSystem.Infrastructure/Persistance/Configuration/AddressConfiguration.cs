using BloodDonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationSystem.Infrastructure.Persistance.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Street).IsRequired();
            builder.Property(a => a.Number).IsRequired();
            builder.Property(a => a.City).IsRequired();
            builder.Property(a => a.State).IsRequired();
            builder.Property(a => a.Cep).IsRequired();

            builder.HasOne(a => a.Donor)
                .WithOne(d => d.Address)
                .HasForeignKey<Address>(a => a.DonorId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
