using BloodDonationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BloodDonationSystem.Infrastructure.Persistance
{
    public class BloodDonationDbContext : DbContext
    {
        public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Donation> Donation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
