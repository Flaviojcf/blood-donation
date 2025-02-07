using BloodDonationSystem.Domain.Entities;

namespace BloodDonationSystem.Domain.Repositories
{
    public interface IDonorRepository : IBaseRepository<Donor>
    {
        Task<Donor> GetByEmail(string email);
        Task<Donor> GetByEmailAndPasswordAsync(string email, string password);
    }
}
