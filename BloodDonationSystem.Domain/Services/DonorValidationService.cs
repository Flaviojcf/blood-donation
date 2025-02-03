using BloodDonationSystem.Domain.Repositories;

namespace BloodDonationSystem.Domain.Services
{
    public class DonorValidationService : IDonorValidationService
    {
        private readonly IDonorRepository _donorRepository;

        public DonorValidationService(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<bool> DonorExistsAsync(Guid donorId)
        {
            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (donor == null) return false;

            return true;
        }
    }
}
