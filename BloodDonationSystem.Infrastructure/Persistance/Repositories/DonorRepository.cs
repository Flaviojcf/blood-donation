using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.Infrastructure.Persistance.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodDonationDbContext _bloodDonationDbContext;
        public DonorRepository(BloodDonationDbContext bloodDonationDbContext)
        {
            _bloodDonationDbContext = bloodDonationDbContext;
        }

        public async Task CreateAsync(Donor entity)
        {
            await _bloodDonationDbContext.Donor.AddAsync(entity);

            await SaveChangesAsync();
        }

        public async Task<List<Donor>> GetAllAsync()
        {
            return await _bloodDonationDbContext.Donor.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Donor> GetByEmail(string email)
        {
            return await _bloodDonationDbContext.Donor.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Donor> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _bloodDonationDbContext.Donor.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<Donor> GetByIdAsync(Guid id)
        {
            return await _bloodDonationDbContext.Donor.Include(d => d.Address).Include(d => d.Donations).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _bloodDonationDbContext.SaveChangesAsync();
        }
    }
}
