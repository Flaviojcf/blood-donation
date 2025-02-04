using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;

namespace BloodDonationSystem.Domain.Services
{
    public class DonorValidationService(IDonorRepository donorRepository) : IDonorValidationService
    {
        private readonly IDonorRepository _donorRepository = donorRepository;

        public async Task<bool> IsDonorExistsAsync(Guid donorId)
        {
            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (donor == null) return false;

            return true;
        }

        public async Task<bool> IsDonorEmailAlreadyExistsAsync(string donorEmail)
        {
            var donor = await _donorRepository.GetByEmail(donorEmail);

            if (donor == null) return false;

            return true;
        }
    }
}
