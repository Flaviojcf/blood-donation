using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.Infrastructure.Persistance.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly BloodDonationDbContext _bloodDonationDbContext;
        public DonationRepository(BloodDonationDbContext bloodDonationDbContext)
        {
            _bloodDonationDbContext = bloodDonationDbContext;
        }

        public async Task CreateAsync(Donation entity)
        {
            await _bloodDonationDbContext.Donation.AddAsync(entity);

            await SaveChangesAsync();
        }

        public async Task<List<Donation>> GetAllAsync()
        {
            return await _bloodDonationDbContext.Donation.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Donation> GetByIdAsync(Guid id)
        {
            return await _bloodDonationDbContext.Donation.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _bloodDonationDbContext.SaveChangesAsync();
        }
    }
}
