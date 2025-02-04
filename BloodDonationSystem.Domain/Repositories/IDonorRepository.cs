using BloodDonationSystem.Domain.Entities;

namespace BloodDonationSystem.Domain.Repositories
{
    public interface IDonorRepository : IBaseRepository<Donor>
    {
        Task<Donor> GetByEmail(string email);
    }
}
