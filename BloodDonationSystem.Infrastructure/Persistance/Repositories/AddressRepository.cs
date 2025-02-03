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

            await _bloodDonationDbContext.SaveChangesAsync();
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _bloodDonationDbContext.Address.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            return await _bloodDonationDbContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _bloodDonationDbContext.SaveChangesAsync();
        }
    }
}
