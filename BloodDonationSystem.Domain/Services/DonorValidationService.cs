using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Enums;
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

        public async Task<bool> IsLegalForDonation(Guid donorId)
        {
            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (!IsLegalAgeForDonation(donor.BirthDate)) return false;
            if (!IsLegalWeightForDonation(donor.Weight)) return false;
            if (donor.Donations == null) return true;
            if (!IsLegalDateForDonation(donor.GenderType, donor.Donations)) return false;

            return true;
        }

        private bool IsLegalAgeForDonation(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (age < 18) return false;

            return true;
        }

        private bool IsLegalWeightForDonation(decimal weight)
        {
            if (weight < 50) return false;
            return true;
        }

        private bool IsLegalDateForDonation(GenderType genderType, IEnumerable<Donation> donations)
        {
            var lastDonation = donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();

            var daysSinceLastDonation = (DateTime.Today - lastDonation.DonationDate).Days;

            var isGreaterThanSixtyDays = daysSinceLastDonation > 60;
            var isGreaterThanNinetyDays = daysSinceLastDonation > 90;
            if (genderType == GenderType.Male && !isGreaterThanSixtyDays) return false;
            if (genderType == GenderType.Female && !isGreaterThanNinetyDays) return false;

            return true;
        }
    }
}
