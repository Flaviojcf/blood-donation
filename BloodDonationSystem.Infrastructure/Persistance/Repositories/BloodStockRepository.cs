using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Enums;
using BloodDonationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.Infrastructure.Persistance.Repositories
{
    public class BloodStockRepository : IBloodStockRepository
    {
        private readonly BloodDonationDbContext _bloodDonationDbContext;

        public BloodStockRepository(BloodDonationDbContext bloodDonationDbContext)
        {
            _bloodDonationDbContext = bloodDonationDbContext;
        }

        public async Task CreateAsync(BloodStock entity)
        {
            await _bloodDonationDbContext.BloodStock.AddAsync(entity);

            await SaveChangesAsync();
        }

        public async Task<List<BloodStock>> GetAllAsync()
        {
            return await _bloodDonationDbContext.BloodStock.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<BloodStock> GetByIdAsync(Guid id)
        {
            return await _bloodDonationDbContext.BloodStock.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _bloodDonationDbContext.SaveChangesAsync();
        }

        public async Task<BloodStock> GetByBloodTypeAndRhFactorAsync(BloodType bloodType, RhFactorType rhFactorType)
        {
            return await _bloodDonationDbContext.BloodStock.SingleOrDefaultAsync(x => x.BloodType == bloodType && x.RhFactorType == rhFactorType);
        }
    }
}
