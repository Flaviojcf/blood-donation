using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Enums;

namespace BloodDonationSystem.Domain.Repositories
{
    public interface IBloodStockRepository : IBaseRepository<BloodStock>
    {
        Task<BloodStock> GetByBloodTypeAndRhFactorAsync(BloodType bloodType, RhFactorType rhFactorType);
    }
}
