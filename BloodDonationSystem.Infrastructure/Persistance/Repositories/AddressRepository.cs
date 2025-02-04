using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.Infrastructure.Persistance.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly BloodDonationDbContext _bloodDonationDbContext;
        public AddressRepository(BloodDonationDbContext bloodDonationDbContext)
        {
            _bloodDonationDbContext = bloodDonationDbContext;
        }

        public async Task CreateAsync(Address entity)
        {
            await _bloodDonationDbContext.Address.AddAsync(entity);

            await SaveChangesAsync();
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _bloodDonationDbContext.Address.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            return await _bloodDonationDbContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> GetByDonorIdAsync(Guid DonorId)
        {
            return await _bloodDonationDbContext.Address.SingleOrDefaultAsync(x => x.DonorId == DonorId);
        }

        public async Task<bool> IsDonorAlreadyHasAddressAsync(Guid donorId)
        {
            var address = await GetByDonorIdAsync(donorId);

            if (address == null) return false;

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _bloodDonationDbContext.SaveChangesAsync();
        }
    }
}
