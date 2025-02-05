using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Enums;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using BloodDonationSystem.Domain.Validations;

namespace BloodDonationSystem.Domain.Services
{
    public class DonorValidationService(IDonorRepository donorRepository) : IDonorValidationService
    {
        private readonly IDonorRepository _donorRepository = donorRepository;

        public async Task<ValidationResult> ValidateCreateDonorAsync(string email)
        {
            var result = new ValidationResult();

            if (await IsDonorEmailAlreadyExistsAsync(email))
            {
                result.AddError($"O endereço de e-mail '{email}' já foi cadastrado");
            }

            return result;
        }

        public async Task<ValidationResult> ValidateCreateAddressForDonorAsync(Guid donorId)
        {
            var result = new ValidationResult();

            if (!await IsDonorExistsAsync(donorId))
            {
                result.AddError($"O doador com o id '{donorId}' não existe.");

                return result;
            }

            if (await IsDonorAlreadyHasAddres(donorId))
            {
                result.AddError($"O doardor com o id '{donorId}' já possui endereço cadastrado.");
            }

            return result;
        }

        public async Task<ValidationResult> ValidateDonorForDonationAsync(Guid donorId)
        {
            var result = new ValidationResult();

            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (!await IsDonorExistsAsync(donorId))
            {
                result.AddError($"O doador com o id '{donorId}' não existe.");

                return result;
            }

            if (!IsLegalAgeForDonation(donor.BirthDate))
            {
                result.AddError("O doador não atende ao critério de idade mínima (18 anos).");
            }

            if (!IsLegalWeightForDonation(donor.Weight))
            {
                result.AddError("O doador não atende ao critério de peso mínimo (50kg).");
            }

            if (donor.Donations != null && !IsLegalDateForDonation(donor.GenderType, donor.Donations))
            {
                result.AddError("O intervalo mínimo entre doações não foi respeitado.");
            }

            return result;
        }

        private async Task<bool> IsDonorExistsAsync(Guid donorId)
        {
            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (donor == null) return false;

            return true;
        }

        private async Task<bool> IsDonorEmailAlreadyExistsAsync(string donorEmail)
        {
            var donor = await _donorRepository.GetByEmail(donorEmail);

            if (donor == null) return false;

            return true;
        }

        private async Task<bool> IsDonorAlreadyHasAddres(Guid donorId)
        {
            var donor = await _donorRepository.GetByIdAsync(donorId);

            if (donor.Address != null) return true;

            return false;
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

            if (donations.Count() == 0) return true;

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
