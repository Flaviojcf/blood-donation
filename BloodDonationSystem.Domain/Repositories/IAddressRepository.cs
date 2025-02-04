using BloodDonationSystem.Domain.Entities;

namespace BloodDonationSystem.Domain.Repositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<bool> IsDonorAlreadyHasAddressAsync(Guid donorId);
        Task<Address> GetByDonorIdAsync(Guid DonorId);
    }
}
